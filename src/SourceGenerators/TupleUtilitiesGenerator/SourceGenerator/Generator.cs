using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SourceGenerator
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        readonly HashSet<TupleExpressionSyntax> _tupleSyntaxes = new HashSet<TupleExpressionSyntax>();
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource($"UtilityTypes.cs", UtilityTypes.All.Select(t => t.Code).Merge("\n"));
            
            var result = _tupleSyntaxes.GenerateExtensionClass(context.Compilation);

            File.WriteAllText("aaa.bbb", $"{_tupleSyntaxes.Count}");

            var code = result.ToString();
            context.AddSource($"TupleExtensions.cs", code);
            File.WriteAllText("artifact.tt", $"{code}");
        }

        public void Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(() => new TupleSyntaxReceiver(_tupleSyntaxes));
    }
}

