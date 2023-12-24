using DestallMaterials.WheelProtection.Extensions.Collections;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Concurrent;

namespace DestallMaterials.CodeGeneration.Utilities;

public class SemanticsReceiver : CSharpSyntaxWalker
{
    readonly Dictionary<INamedTypeSymbol, List<ISymbol>> _attributeTiedSymbols;
    readonly Compilation _compilation;

    readonly ConcurrentDictionary<string, SemanticModel> _semanticModels = new();

    public IReadOnlyDictionary<INamedTypeSymbol, IReadOnlyList<ISymbol>> AttributeTiedSymbols
        => _attributeTiedSymbols.ToDictionary(kv => kv.Key, kv => kv.Value.AsReadOnlyList());

    public SemanticsReceiver(Compilation compilation, IEnumerable<string> seekedAttributes)
    {
        _compilation = compilation;
        SymbolEqualityComparer equalityComparer = SymbolEqualityComparer.Default;
        _attributeTiedSymbols = new(equalityComparer);

        foreach (var attr in seekedAttributes)
        {
            var attrSymbol = compilation.GetTypeByMetadataName(attr)
                ?? throw new InvalidOperationException($"Symbol {attr} not found within compilation.");

            _attributeTiedSymbols.Add(attrSymbol, new());
        }
    }

    void OnVisitSyntaxNode(SyntaxNode syntaxNode, SemanticModel treeSemanticModel)
    {
        var soughtAttributes = _attributeTiedSymbols.Keys;
        if (syntaxNode is ClassDeclarationSyntax cds)
        {
            if (cds.AttributeLists.Count > 0 || EnumerableExtensions.HasContent<BaseTypeSyntax>(cds.BaseList?.Types))
            {
                var classSymbol = treeSemanticModel.GetDeclaredSymbol(cds)
                    ?? throw new InvalidOperationException();

                foreach (var attr in soughtAttributes.Where(a => classSymbol.TiedToAttribute(a)))
                {
                    _attributeTiedSymbols[attr].Add(classSymbol);
                }
            }
        }
        else if (syntaxNode is MethodDeclarationSyntax mds && mds.DescendantNodesAndTokens().Any(t => t.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.PublicKeyword)))
        {
            var methodSymbol = treeSemanticModel.GetDeclaredSymbol(mds);

            var presentAttributes = soughtAttributes.Where(attr =>
            {
                var hasAttribute = methodSymbol.HasAttribute(attr);
                if (hasAttribute)
                {
                    return true;
                }

                var parameters = methodSymbol.Parameters;

                return parameters.Any(p => p.Type.TiedToAttribute(attr));
            });

            foreach (var attr in presentAttributes)
            {
                _attributeTiedSymbols[attr].Add(methodSymbol);
            }
        }
    }

    public override void Visit(SyntaxNode node)
    {
        base.Visit(node);

        var tree = node.SyntaxTree;

        if (!_compilation.ContainsSyntaxTree(tree))
        {
            throw new InvalidOperationException("Visited syntax node must belong to the receiver's compilation.");
        }

        SemanticModel semanticModel;
        if (!_semanticModels.TryGetValue(tree.FilePath, out semanticModel))
        {
            semanticModel = _compilation.GetSemanticModel(tree);
            _semanticModels[tree.FilePath] = semanticModel;
        }

        OnVisitSyntaxNode(node, semanticModel);
    }

    public async Task<IReadOnlyDictionary<INamedTypeSymbol, IReadOnlyList<ISymbol>>> VisitAllSyntaxTreesAsync()
    {
        await _compilation.SyntaxTrees.SelectAsync(tree => Task.Run(() =>
        {
            Visit(tree.GetRoot()); 
            return true;
        })).ToListAsync();

        return AttributeTiedSymbols;
    }
}

public static class SemanticsReceiverExtensions
{
    public static IReadOnlyList<ISymbol> GetAttributeSymbols<TAttribute>(this SemanticsReceiver semanticsReceiver)
        => semanticsReceiver.GetAttributeSymbols(typeof(TAttribute).FullName);

    public static IReadOnlyList<ISymbol> GetAttributeSymbols(this SemanticsReceiver semanticsReceiver, string attributeFullName)
        => semanticsReceiver.AttributeTiedSymbols.FirstOrDefault(kv => kv.Key.ToFullDisplayString() == attributeFullName).Value ?? Array.Empty<ISymbol>();
}

public static class SyntaxExtensions
{

}
