using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EverModern.Memory;

public static class AutoPackGenerator
{
    public static void Register(IncrementalGeneratorInitializationContext context)
    {
        var autoPackInfos = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) =>
                {
                    if (node is not ClassDeclarationSyntax cds || cds.BaseList == null)
                        return false;

                    return cds.BaseList.Types.Any(bt =>
                    {
                        var type = bt.Type;
                        while (type is QualifiedNameSyntax qns)
                            type = qns.Right;
                        var name = type is GenericNameSyntax gns ? gns.Identifier.ValueText
                            : type is SimpleNameSyntax sns ? sns.Identifier.ValueText
                            : null;
                        return name == "IAutoPack";
                    });
                },
                transform: static (ctx, ct) =>
                {
                    var classDecl = (ClassDeclarationSyntax)ctx.Node;
                    var classSymbol = ctx.SemanticModel.GetDeclaredSymbol(classDecl, ct) as INamedTypeSymbol;
                    if (classSymbol == null)
                        return default((INamedTypeSymbol ClassSymbol, INamedTypeSymbol TupleArg, int FormatterCount)?);

                    var autoPackInterface = classSymbol.AllInterfaces
                        .FirstOrDefault(i => i.Name == "IAutoPack"
                            && i.ContainingNamespace.ToDisplayString() == "EverModern.Memory");

                    if (autoPackInterface == null || autoPackInterface.TypeArguments.Length == 0)
                        return default((INamedTypeSymbol ClassSymbol, INamedTypeSymbol TupleArg, int FormatterCount)?);

                    var typeArg = autoPackInterface.TypeArguments[0] as INamedTypeSymbol;
                    if (typeArg == null)
                        return default((INamedTypeSymbol ClassSymbol, INamedTypeSymbol TypeArg, int FormatterCount)?);

                    var typeCount = AutoPackTypeResolver.ResolveTypes(typeArg, ctx.SemanticModel.Compilation, ct).Count;

                    return ((INamedTypeSymbol ClassSymbol, INamedTypeSymbol TypeArgument, int FormatterCount)?)
                        (ClassSymbol: classSymbol, TypeArgument: typeArg, FormatterCount: typeCount);
                })
            .Where(x => x != null)
            .Select((x, _) => x!.Value);

        // Per-contract generation
        context.RegisterSourceOutput(
            autoPackInfos.Combine(context.CompilationProvider),
            (ctx, data) =>
            {
                var (info, compilation) = (data.Left, data.Right);

                try
                {
                    var code = AutoPackCodeEmitter.Generate(
                        info.Item1, info.Item2, compilation, ctx.CancellationToken);

                    if (code != null)
                    {
                        var hintName = $"{info.Item1.ToDisplayString(
                            SymbolDisplayFormat.FullyQualifiedFormat
                                .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted))
                            }.AutoPack.g.cs";
                        ctx.AddSource(hintName, code);
                    }
                }
                catch (Exception e)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        new DiagnosticDescriptor("AG001", "AutoPack Error",
                            $"{e}", "AutoPack", DiagnosticSeverity.Error, true),
                        Location.None));
                }
            });

        // Emit the RegisterMany extensions class covering max formatter count
        context.RegisterSourceOutput(
            autoPackInfos.Select((x, _) => x.Item3).Collect(),
            (ctx, counts) =>
            {
                var maxCount = counts.Length > 0 ? counts.Max() : 0;
                if (maxCount < 2)
                    return;

                ctx.AddSource("MemoryPackFormatterProviderExtensions.g.cs",
                        AutoPackCodeEmitter.EmitRegisterManyExtensions(maxCount));
            });
    }
}

