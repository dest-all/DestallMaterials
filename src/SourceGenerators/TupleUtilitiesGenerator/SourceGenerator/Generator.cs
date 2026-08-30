using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EverModern.SyntaxGenerator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(ctx =>
            {
                ctx.AddSource($"UtilityTypes.cs", UtilityTypes.Nothing.Code);
                ctx.AddSource("TaskTupleAwaiter.g.cs", UtilityTypes.TaskTupleAwaiter.Code);
                ctx.AddSource("TupleExtensions.GlobalUsings.g.cs",
                    $"global using {TupleCodeGeneration.ExtensionNamespace};\n");
                ctx.AddSource("GenericTaskTupleAwaiters.g.cs",
                    $"namespace {TupleCodeGeneration.ExtensionNamespace}\n{{\n\tpublic static partial class {TupleCodeGeneration.ExtensionClassName}\n{{\n{TupleCodeGeneration.PreGeneratedGenericAwaiters}\t}}\n}}");
            });

            // Syntax-only pipeline
            var tupleExpressions = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (node, _) => node is TupleExpressionSyntax,
                    transform: static (ctx, _) => (TupleExpressionSyntax)ctx.Node)
                .Collect();

            context.RegisterSourceOutput(tupleExpressions, (ctx, tuples) =>
            {
                try
                {
                    var tupleSyntaxes = new HashSet<TupleExpressionSyntax>(tuples);
                    var result = tupleSyntaxes.GenerateExtensionClass();

                    var code = result.ToString();
                    ctx.AddSource($"DestallTupleExtensions.cs", code);
                }
                catch (System.Exception e)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("SG001", "Error", $"{e}", "Error", DiagnosticSeverity.Error, true), Location.None));
                    File.WriteAllText("artifact.tt", $"{e}");
                }
            });

            // Semantic pipeline: generate GetAwaiter methods for tuples of Task/ValueTask.
            // We look at both TupleExpressionSyntax (for literal tuples of tasks) and
            // AwaitExpressionSyntax (for tuples produced by .Select() or method calls).
            var taskTuples = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (node, _) => node is TupleExpressionSyntax or AwaitExpressionSyntax,
                    transform: static (ctx, ct) =>
                    {
                        var type = ctx.Node switch
                        {
                            TupleExpressionSyntax tes => ctx.SemanticModel.GetTypeInfo(tes, ct).Type,
                            AwaitExpressionSyntax aes => ctx.SemanticModel.GetTypeInfo(aes.Expression, ct).Type,
                            _ => null
                        };
                        return type as INamedTypeSymbol;
                    })
                .Where(t => t != null && t.IsTupleType)
                .Collect();

            context.RegisterSourceOutput(taskTuples, (ctx, tupleTypes) =>
            {
                try
                {
                    var distinct = tupleTypes.Distinct<INamedTypeSymbol>(SymbolEqualityComparer.Default);
                    var methods = new StringBuilder();

                    foreach (var tupleType in distinct)
                    {
                        var method = TupleCodeGeneration.GenerateTaskTupleGetAwaiter(tupleType);
                        if (method != null)
                        {
                            methods.AppendLine($"\t\t{method}");
                        }
                    }

                    if (methods.Length > 0)
                    {
                        var code = $"namespace {TupleCodeGeneration.ExtensionNamespace}\n{{\n\tpublic static partial class {TupleCodeGeneration.ExtensionClassName}\n{{\n{methods}\t}}\n}}";
                        ctx.AddSource("TaskTupleAwaiters.g.cs", code);
                    }
                }
                catch (System.Exception e)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("SG002", "Error", $"{e}", "Error", DiagnosticSeverity.Error, true), Location.None));
                }
            });
        }
    }
}

