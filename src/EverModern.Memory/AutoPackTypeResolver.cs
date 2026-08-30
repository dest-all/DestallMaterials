using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace EverModern.Memory;

public static class AutoPackTypeResolver
{
    public static List<INamedTypeSymbol> ResolveTypes(
        INamedTypeSymbol typeArg,
        Compilation compilation,
        CancellationToken ct)
    {
        var result = new List<INamedTypeSymbol>();
        var seen = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        // Collect the type(s) from the argument — works for both tuples and single types
        var inputTypes = typeArg.IsTupleType
            ? typeArg.TupleElements.Select(e => e.Type as INamedTypeSymbol).Where(t => t != null).Cast<INamedTypeSymbol>().ToList()
            : new List<INamedTypeSymbol> { typeArg };

        foreach (var type in inputTypes)
        {
            if (!seen.Add(type))
                continue;

            result.Add(type);

            if (type.IsAbstract)
            {
                var allTypes = GetAllTypesInCompilation(compilation, ct);
                foreach (var t in allTypes)
                {
                    if (t.IsAbstract || t.TypeKind != TypeKind.Class)
                        continue;
                    if (seen.Contains(t))
                        continue;
                    if (IsDerivedFrom(t, type))
                    {
                        seen.Add(t);
                        result.Add(t);
                    }
                }
            }
        }

        return result;
    }

    public static List<INamedTypeSymbol> DeterminePriorityOrder(
        List<INamedTypeSymbol> allTypes,
        INamedTypeSymbol typeArg,
        Compilation compilation)
    {
        var abstractBase = allTypes.FirstOrDefault(t => t.IsAbstract);
        var derivedTypes = allTypes.Where(t => !t.IsAbstract).ToList();

        if (abstractBase == null)
            return allTypes;

        var priorityMap = new Dictionary<INamedTypeSymbol, int>(SymbolEqualityComparer.Default);
        foreach (var derived in derivedTypes)
        {
            var priorityAttr = derived.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.Name == "PrioryAttribute"
                    && a.AttributeClass.ContainingNamespace.ToDisplayString() == "EverModern.Memory");

            if (priorityAttr != null && priorityAttr.ConstructorArguments.Length > 0)
                priorityMap[derived] = (int)priorityAttr.ConstructorArguments[0].Value!;
        }

        // Collect explicitly listed types from the type argument (tuple or single)
        var tupleTypes = typeArg.IsTupleType
            ? typeArg.TupleElements
                .Select(e => e.Type as INamedTypeSymbol)
                .Where(t => t != null)
                .Select((t, i) => (Type: t!, Index: i))
                .ToList()
            : new List<(INamedTypeSymbol Type, int Index)> { (typeArg, 0) };

        var tupleTypeSet = new HashSet<INamedTypeSymbol>(
            tupleTypes.Select(t => t.Type), SymbolEqualityComparer.Default);

        derivedTypes.Sort((a, b) =>
        {
            var aInTuple = tupleTypeSet.Contains(a);
            var bInTuple = tupleTypeSet.Contains(b);

            if (aInTuple && bInTuple)
            {
                var aIdx = tupleTypes.FindIndex(t => SymbolEqualityComparer.Default.Equals(t.Type, a));
                var bIdx = tupleTypes.FindIndex(t => SymbolEqualityComparer.Default.Equals(t.Type, b));
                return aIdx.CompareTo(bIdx);
            }

            if (aInTuple) return -1;
            if (bInTuple) return 1;

            var aPri = priorityMap.TryGetValue(a, out var ap) ? ap : int.MaxValue;
            var bPri = priorityMap.TryGetValue(b, out var bp) ? bp : int.MaxValue;
            if (aPri != bPri)
                return aPri.CompareTo(bPri);

            return string.Compare(a.Name, b.Name, System.StringComparison.Ordinal);
        });

        var ordered = new List<INamedTypeSymbol> { abstractBase };
        ordered.AddRange(derivedTypes);
        return ordered;
    }

    public static Dictionary<INamedTypeSymbol, string> ResolveNameClashes(List<INamedTypeSymbol> types)
    {
        var names = new Dictionary<INamedTypeSymbol, string>(SymbolEqualityComparer.Default);
        var nameCounts = new Dictionary<string, int>();

        foreach (var type in types)
        {
            var baseName = type.Name;
            nameCounts.TryGetValue(baseName, out var count);
            nameCounts[baseName] = count + 1;
            names[type] = baseName;
        }

        var clashes = nameCounts.Where(kv => kv.Value > 1).Select(kv => kv.Key).ToHashSet();
        if (clashes.Count == 0)
            return names;

        var clashTypes = types.Where(t => clashes.Contains(t.Name)).ToList();
        var resolvedClashNames = new HashSet<string>();

        foreach (var type in clashTypes)
        {
            var nsParts = type.ContainingNamespace.IsGlobalNamespace
                ? System.Array.Empty<string>()
                : type.ContainingNamespace.ToDisplayString().Split('.').Reverse().ToArray();

            var candidate = type.Name;
            var suffixIndex = 0;

            while (resolvedClashNames.Contains(candidate) || names.Values.Count(v => v == candidate) > 1)
            {
                if (suffixIndex < nsParts.Length)
                    candidate = nsParts[suffixIndex] + type.Name;
                else
                {
                    candidate = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                        .Replace(".", "_").Replace("<", "_").Replace(">", "_");
                    break;
                }
                suffixIndex++;
            }

            resolvedClashNames.Add(candidate);
            names[type] = candidate;
        }

        return names;
    }

    public static List<IPropertySymbol> GetSerializableProperties(INamedTypeSymbol type)
    {
        var properties = new List<IPropertySymbol>();
        var current = type;
        var seen = new HashSet<string>();

        while (current != null && current.SpecialType != SpecialType.System_Object)
        {
            foreach (var member in current.GetMembers())
            {
                if (member is IPropertySymbol prop
                    && prop.DeclaredAccessibility == Accessibility.Public
                    && !prop.IsStatic
                    && !prop.IsIndexer
                    && prop.GetMethod != null
                    && seen.Add(prop.Name))
                {
                    properties.Add(prop);
                }
            }
            current = current.BaseType;
        }

        return properties;
    }

    public static bool IsDerivedFrom(INamedTypeSymbol type, INamedTypeSymbol baseType)
    {
        var current = type.BaseType;
        while (current != null)
        {
            if (SymbolEqualityComparer.Default.Equals(current, baseType))
                return true;
            current = current.BaseType;
        }
        return false;
    }

    public static bool IsWriteValueType(ITypeSymbol type)
    {
        return type.SpecialType is
            SpecialType.System_Int32 or SpecialType.System_Int64 or
            SpecialType.System_Boolean or SpecialType.System_DateTime or
            SpecialType.System_Double or SpecialType.System_Single or
            SpecialType.System_Byte or SpecialType.System_Int16 or
            SpecialType.System_UInt32 or SpecialType.System_UInt64 or
            SpecialType.System_Char or SpecialType.System_Decimal or
            SpecialType.System_SByte or SpecialType.System_UInt16;
    }

    public static List<IPropertySymbol> GetPropertiesInConstructorOrder(
        INamedTypeSymbol type,
        List<IPropertySymbol> properties)
    {
        var constructors = type.Constructors
            .Where(c => !c.IsStatic && c.DeclaredAccessibility == Accessibility.Public)
            .OrderBy(c => c.Parameters.Length)
            .ToList();

        if (constructors.Count == 0)
            return properties;

        var ctor = constructors.FirstOrDefault(c =>
            c.Parameters.All(p => properties.Any(pr => pr.Name == p.Name)));
        ctor ??= constructors.First();

        var ordered = new List<IPropertySymbol>();
        var remaining = new HashSet<IPropertySymbol>(properties, SymbolEqualityComparer.Default);

        foreach (var param in ctor.Parameters)
        {
            var match = remaining.FirstOrDefault(p => p.Name == param.Name);
            if (match != null)
            {
                ordered.Add(match);
                remaining.Remove(match);
            }
        }

        ordered.AddRange(remaining);
        return ordered;
    }

    public static bool TryMatchConstructor(
        INamedTypeSymbol type,
        List<IPropertySymbol> properties,
        out IMethodSymbol ctor,
        out List<(string ParamName, string? PropName)> paramMap)
    {
        ctor = null!;
        paramMap = new();

        var constructors = type.Constructors
            .Where(c => !c.IsStatic && c.DeclaredAccessibility == Accessibility.Public)
            .OrderBy(c => c.Parameters.Length)
            .ToList();

        if (constructors.Count == 0)
            return false;

        ctor = constructors.FirstOrDefault(c =>
            c.Parameters.All(p => properties.Any(pr => pr.Name == p.Name)));
        ctor ??= constructors.First();

        foreach (var param in ctor.Parameters)
        {
            var matchingProp = properties.FirstOrDefault(pr => pr.Name == param.Name);
            paramMap.Add((param.Name, matchingProp?.Name));
        }

        return true;
    }

    private static List<INamedTypeSymbol> GetAllTypesInCompilation(Compilation compilation, CancellationToken ct)
    {
        var result = new List<INamedTypeSymbol>();
        GetTypesInNamespace(compilation.GlobalNamespace, result);
        return result;

        static void GetTypesInNamespace(INamespaceSymbol ns, List<INamedTypeSymbol> result)
        {
            foreach (var member in ns.GetMembers())
            {
                if (member is INamespaceSymbol childNs)
                    GetTypesInNamespace(childNs, result);
                else if (member is INamedTypeSymbol type)
                {
                    result.Add(type);
                    GetNestedTypes(type, result);
                }
            }
        }

        static void GetNestedTypes(INamedTypeSymbol type, List<INamedTypeSymbol> result)
        {
            foreach (var nested in type.GetTypeMembers())
            {
                result.Add(nested);
                GetNestedTypes(nested, result);
            }
        }
    }
}
