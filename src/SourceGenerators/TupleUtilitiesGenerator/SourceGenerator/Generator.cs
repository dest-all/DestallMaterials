﻿using System.Collections.Generic;
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
            //context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("SG001", "Success", "Success", "Success", DiagnosticSeverity.Error, true), Location.None));

            context.AddSource($"UtilityTypes.cs", UtilityTypes.All.Select(t => t.Code).Merge("\n"));

            try
            {
                var result = _tupleSyntaxes.GenerateExtensionClass(context.Compilation);

                var code = result.ToString();
                context.AddSource($"DestallTupleExtensions.cs", code);
                //File.WriteAllText("DestallTupleExtensions.artifact", $"{code}");

                //context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("SG001", "Success", "Success", "Success", DiagnosticSeverity.Error, true), Location.None));
            }
            catch (System.Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("SG001", "Error", $"{e}", "Error", DiagnosticSeverity.Error, true), Location.None));
                File.WriteAllText("artifact.tt", $"{e}");
            }
        }

        public void Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(() => new TupleSyntaxReceiver(_tupleSyntaxes));
    }
}

