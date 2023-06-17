using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationExample.Generator.Lib
{
    class SyntaxReceiver : CSharpSyntaxWalker, ISyntaxReceiver
    {
        public readonly List<ClassDeclarationSyntax> AttributeBearingClasses = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax cds)
            {
                if (cds.AttributeLists.Count > 0 || true)
                {
                    AttributeBearingClasses.Add(cds);
                }
            }
        }

        public override void Visit(SyntaxNode node)
        {
            base.Visit(node);
            OnVisitSyntaxNode(node);
        }
    }
}
