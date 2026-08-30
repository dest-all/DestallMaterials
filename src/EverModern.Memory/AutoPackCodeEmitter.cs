using System.Text;
using Microsoft.CodeAnalysis;

namespace EverModern.Memory;

public static class AutoPackCodeEmitter
{
    public static string? Generate(
        INamedTypeSymbol classSymbol,
        INamedTypeSymbol typeArg,
        Compilation compilation,
        CancellationToken ct)
    {
        var allTypes = AutoPackTypeResolver.ResolveTypes(typeArg, compilation, ct);
        if (allTypes.Count == 0)
            return null;

        var orderedTypes = AutoPackTypeResolver.DeterminePriorityOrder(allTypes, typeArg, compilation);
        var resolvedNames = AutoPackTypeResolver.ResolveNameClashes(orderedTypes);

        var abstractBase = orderedTypes.FirstOrDefault(t => t.IsAbstract);
        var unionTagMap = new Dictionary<INamedTypeSymbol, int>(SymbolEqualityComparer.Default);
        if (abstractBase != null)
        {
            for (int i = 0; i < orderedTypes.Count; i++)
                unionTagMap[orderedTypes[i]] = i;
        }

        var ctx = new EmitterContext(resolvedNames, unionTagMap, abstractBase);

        var body = new StringBuilder();
        foreach (var type in orderedTypes)
        {
            body.AppendLine();
            body.Append(EmitFormatterClass(type, ctx));
        }
        body.AppendLine();
        foreach (var type in orderedTypes)
            body.Append(EmitFactoryMethod(type, ctx));

        body.AppendLine();
        body.Append(EmitCreateFormattersMethod(orderedTypes, ctx));

        var ns = classSymbol.ContainingNamespace.IsGlobalNamespace
            ? null
            : classSymbol.ContainingNamespace.ToDisplayString();

        return WrapInNamespaceAndClass(ns, classSymbol.Name, body.ToString());
    }

    static string WrapInNamespaceAndClass(string? ns, string className, string body)
    {
        var classBlock = $$"""
            #nullable enable
            #pragma warning disable CS8600, CS8604
            using EverModern.Memory;

            partial class {{className}}
            {
            {{Indent(body, 1)}}
            }
            """;

        if (ns == null)
            return classBlock;

        return $$"""
            namespace {{ns}}
            {
            {{Indent(classBlock, 1)}}
            }
            """;
    }

    static string EmitFactoryMethod(INamedTypeSymbol type, EmitterContext ctx)
    {
        var n = ctx.ResolvedNames[type];
        var t = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return $$"""
            public MemoryPack.MemoryPackFormatter<{{t}}> Create{{n}}Formatter()
            {
                return new {{n}}Formatter();
            }
            """;
    }

    static string EmitCreateFormattersMethod(List<INamedTypeSymbol> types, EmitterContext ctx)
    {
        var items = types.Select(t =>
        {
            var n = ctx.ResolvedNames[t];
            return $"new {n}Formatter()";
        }).ToList();

        var tupleType = types.Select(t =>
        {
            var td = t.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            return $"MemoryPack.MemoryPackFormatter<{td}>";
        }).ToList();

        // For a single type, return the formatter directly (not a tuple)
        if (types.Count == 1)
        {
            return $$"""
                public {{tupleType[0]}} CreateFormatters()
                {
                    return {{items[0]}};
                }
                """;
        }

        return $$"""
            public ({{string.Join(", ", tupleType)}}) CreateFormatters()
            {
                return ({{string.Join(", ", items)}});
            }
            """;
    }

    static string EmitFormatterClass(INamedTypeSymbol type, EmitterContext ctx)
    {
        var n = ctx.ResolvedNames[type];
        var t = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        if (type.IsAbstract)
            return $$"""
            private sealed class {{n}}Formatter : MemoryPack.MemoryPackFormatter<{{t}}>
            {
            {{Indent(EmitAbstractBody(type, ctx), 1)}}
            }
            """;

        var props = AutoPackTypeResolver.GetPropertiesInConstructorOrder(
            type, AutoPackTypeResolver.GetSerializableProperties(type));
        return $$"""
        private sealed class {{n}}Formatter : MemoryPack.MemoryPackFormatter<{{t}}>
        {
        {{Indent(EmitConcreteBody(type, props, ctx), 1)}}
        }
        """;
    }

    static string EmitAbstractBody(INamedTypeSymbol type, EmitterContext ctx)
    {
        var t = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var derived = ctx.UnionTagMap.Keys
            .Where(d => !d.IsAbstract && AutoPackTypeResolver.IsDerivedFrom(d, type))
            .OrderBy(d => ctx.UnionTagMap[d])
            .ToList();

        return $$"""
        public override void Serialize<TBufferWriter>(ref MemoryPack.MemoryPackWriter<TBufferWriter> writer, scoped ref {{t}}? value)
        {
            if (value == null)
            {
                writer.WriteNullUnionHeader();
                return;
            }
        {{Indent(EmitUnionSerializeSwitch(derived, ctx), 1)}}
        }

        public override void Deserialize(ref MemoryPack.MemoryPackReader reader, scoped ref {{t}}? value)
        {
            if (!reader.TryReadUnionHeader(out var tag))
            {
                value = default;
                return;
            }
        {{Indent(EmitUnionDeserializeSwitch(derived, ctx), 1)}}
        }
        """;
    }

    static string EmitUnionSerializeSwitch(List<INamedTypeSymbol> derived, EmitterContext ctx)
    {
        if (derived.Count == 0)
            return "writer.WriteNullUnionHeader();";

        var sb = new StringBuilder();
        sb.AppendLine("switch (value)");
        sb.AppendLine("{");
        foreach (var d in derived)
        {
            var dt = d.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var dn = ctx.ResolvedNames[d];
            var tag = ctx.UnionTagMap[d];
            sb.AppendLine($"    case {dt} derived_{tag}:");
            sb.AppendLine($"        writer.WriteUnionHeader({tag});");
            sb.AppendLine($"        new {dn}Formatter().Serialize(ref writer, ref derived_{tag});");
            sb.AppendLine($"        break;");
        }
        sb.AppendLine("    default:");
        sb.AppendLine("        writer.WriteNullUnionHeader();");
        sb.AppendLine("        break;");
        sb.AppendLine("}");
        return sb.ToString();
    }

    static string EmitUnionDeserializeSwitch(List<INamedTypeSymbol> derived, EmitterContext ctx)
    {
        if (derived.Count == 0)
            return "value = default;";

        var sb = new StringBuilder();
        sb.AppendLine("switch (tag)");
        sb.AppendLine("{");
        foreach (var d in derived)
        {
            var dt = d.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var dn = ctx.ResolvedNames[d];
            var tag = ctx.UnionTagMap[d];
            sb.AppendLine($"    case {tag}:");
            sb.AppendLine($"    {{");
            sb.AppendLine($"        {dt}? typed = default;");
            sb.AppendLine($"        new {dn}Formatter().Deserialize(ref reader, ref typed);");
            sb.AppendLine($"        value = typed;");
            sb.AppendLine($"        break;");
            sb.AppendLine($"    }}");
        }
        sb.AppendLine("    default:");
        sb.AppendLine("        value = default;");
        sb.AppendLine("        break;");
        sb.AppendLine("}");
        return sb.ToString();
    }

    static string EmitConcreteBody(INamedTypeSymbol type, List<IPropertySymbol> props, EmitterContext ctx)
    {
        var t = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return $$"""
        public override void Serialize<TBufferWriter>(ref MemoryPack.MemoryPackWriter<TBufferWriter> writer, scoped ref {{t}}? value)
        {
            if (value == null)
            {
                writer.WriteNullObjectHeader();
                return;
            }

            writer.WriteObjectHeader({{props.Count}});
        {{Indent(EmitPropertyWrites(props, ctx), 1)}}
        }

        public override void Deserialize(ref MemoryPack.MemoryPackReader reader, scoped ref {{t}}? value)
        {
            if (!reader.TryReadObjectHeader(out var count))
            {
                value = default;
                return;
            }
        {{Indent(EmitPropertyReads(type, props, ctx), 1)}}
        }
        """;
    }

    static string EmitPropertyWrites(List<IPropertySymbol> props, EmitterContext ctx)
    {
        var sb = new StringBuilder();
        foreach (var p in props)
        {
            var name = p.Name;
            var pt = p.Type;

            if (pt.SpecialType == SpecialType.System_String)
                sb.AppendLine($"writer.WriteString(value.{name});");
            else if (AutoPackTypeResolver.IsWriteValueType(pt) || pt.IsValueType)
                sb.AppendLine($"writer.WriteValue(value.{name});");
            else if (pt is INamedTypeSymbol nts && ctx.ResolvedNames.TryGetValue(nts, out var fn))
            {
                sb.AppendLine($"var __tmp_{name} = value.{name};");
                sb.AppendLine($"new {fn}Formatter().Serialize(ref writer, ref __tmp_{name});");
            }
            else
            {
                sb.AppendLine($"var __tmp_{name} = value.{name};");
                sb.AppendLine($"MemoryPack.MemoryPackSerializer.Serialize(ref writer, __tmp_{name});");
            }
        }
        return sb.ToString();
    }

    static string EmitPropertyReads(INamedTypeSymbol type, List<IPropertySymbol> props, EmitterContext ctx)
    {
        var locals = new List<(string Name, IPropertySymbol Prop)>();
        var sb = new StringBuilder();

        foreach (var p in props)
        {
            var local = $"__{char.ToLower(p.Name[0])}{p.Name.Substring(1)}";
            EmitReadLine(sb, p, local, ctx);
            locals.Add((local, p));
        }

        sb.AppendLine();
        sb.Append(EmitConstructValue(type, locals));
        return sb.ToString();
    }

    static void EmitReadLine(StringBuilder sb, IPropertySymbol prop, string local, EmitterContext ctx)
    {
        var pt = prop.Type;
        var td = pt.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        if (pt.SpecialType == SpecialType.System_String)
            sb.AppendLine($"{td}? {local} = reader.ReadString();");
        else if (AutoPackTypeResolver.IsWriteValueType(pt))
            sb.AppendLine($"{td} {local} = reader.ReadValue<{td}>();");
        else if (pt.IsValueType)
            sb.AppendLine($"var {local} = reader.ReadValue<{td}>();");
        else if (pt is INamedTypeSymbol nts && ctx.ResolvedNames.TryGetValue(nts, out var fn))
        {
            sb.AppendLine($"{td}? {local} = default;");
            sb.AppendLine($"new {fn}Formatter().Deserialize(ref reader, ref {local});");
        }
        else
        {
            sb.AppendLine($"{td}? {local} = reader.ReadValue<{td}>();");
        }
    }

    static string EmitConstructValue(INamedTypeSymbol type,
        List<(string Name, IPropertySymbol Prop)> locals)
    {
        var t = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var sb = new StringBuilder();

        if (!AutoPackTypeResolver.TryMatchConstructor(type, locals.Select(l => l.Prop).ToList(),
                out var ctor, out var paramMap))
        {
            sb.AppendLine($"value = new {t}();");
            foreach (var (name, prop) in locals)
            {
                if (prop.SetMethod != null || prop.IsRequired)
                    sb.AppendLine($"value.{prop.Name} = {name};");
            }
            return sb.ToString();
        }

        var ctorArgs = paramMap.Select(pm =>
        {
            if (pm.PropName != null)
            {
                var match = locals.FirstOrDefault(l => l.Prop.Name == pm.PropName);
                if (match.Name != null)
                    return $"{pm.ParamName}: {match.Name}";
            }
            return $"{pm.ParamName}: default";
        });

        sb.AppendLine($"value = new {t}({string.Join(", ", ctorArgs)});");

        foreach (var (name, prop) in locals)
        {
            if (prop.SetMethod != null && !paramMap.Any(pm => pm.PropName == prop.Name))
                sb.AppendLine($"value.{prop.Name} = {name};");
        }
        return sb.ToString();
    }

    static string Indent(string text, int levels)
    {
        if (string.IsNullOrEmpty(text) || levels <= 0)
            return text;
        var prefix = new string(' ', levels * 4);
        var lines = text.Split('\n');
        return string.Join("\n", lines.Select(l => string.IsNullOrEmpty(l) ? l : prefix + l));
    }

    public static string EmitRegisterManyExtensions(int maxArity)
    {
        if (maxArity < 2)
            return "";

        var sb = new StringBuilder();
        sb.AppendLine("using MemoryPack;");
        sb.AppendLine();
        sb.AppendLine("namespace EverModern.Memory");
        sb.AppendLine("{");
        sb.AppendLine("#pragma warning disable CS1591");
        sb.AppendLine("    public static partial class MemoryPackFormatterProviderExtensions");
        sb.AppendLine("    {");
        sb.AppendLine("        extension(MemoryPackFormatterProvider)");
        sb.AppendLine("        {");

        for (int arity = 2; arity <= maxArity; arity++)
        {
            var tParams = string.Join(", ", Enumerable.Range(1, arity).Select(i => $"T{i}"));
            var tupleMembers = string.Join(", ", Enumerable.Range(1, arity).Select(i => $"MemoryPackFormatter<T{i}>"));
            var registers = string.Join("\n",
                Enumerable.Range(1, arity).Select(i => $"                MemoryPackFormatterProvider.Register(formatters.Item{i});"));

            sb.AppendLine();
            sb.AppendLine($"            public static void RegisterMany<{tParams}>(");
            sb.AppendLine($"                ({tupleMembers}) formatters)");
            sb.AppendLine("            {");
            sb.AppendLine(registers);
            sb.AppendLine("            }");
        }

        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }
}

public sealed class EmitterContext
{
    public EmitterContext(
        Dictionary<INamedTypeSymbol, string> resolvedNames,
        Dictionary<INamedTypeSymbol, int> unionTagMap,
        INamedTypeSymbol? abstractBase)
    {
        ResolvedNames = resolvedNames;
        UnionTagMap = unionTagMap;
        AbstractBase = abstractBase;
    }

    public Dictionary<INamedTypeSymbol, string> ResolvedNames { get; }
    public Dictionary<INamedTypeSymbol, int> UnionTagMap { get; }
    public INamedTypeSymbol? AbstractBase { get; }
}
