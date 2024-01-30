using DestallMaterials.CodeGeneration;
using DestallMaterials.CodeGeneration.Utilities;
using DestallMaterials.WheelProtection.Extensions.Objects;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using DestallMaterials.WheelProtection.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TestComponents;
using static Microsoft.Build.Utilities.SDKManifest;


namespace CodeGenerationTests;

[Parallelizable(scope: ParallelScope.All)]
public class UtilitiesTesting : CodeGenerationTests
{
    [Test]
    public async Task CollectSymbolsWithAttributes()
    {
        // Arrange
        using var workspace = CodeGenerationWorkspace.Create(_supplierProject);
        var renderer = new SourceFileRenderer(workspace, sp => { });
        var projectName = workspace.ProjectLocations.Keys.First(_supplierProject.Contains);

        var attribute = typeof(ObsoleteAttribute);

        const string parameterMethodName = "NewMethod";
        const string attributedMethodName = "AttributedMethod";
        const string heirClass = "HeirClass";

        var newFile = GeneratedFile(projectName) with 
        {
            Path = new ProjectRelativeFilePath(projectName, fileName),
            Content = $@"
namespace {nameSpace};

[{attribute.FullName}]
public class {className}
{{
    
}}

public class {heirClass} : {className}
{{
}}

public static class Functions
{{
    public static void {parameterMethodName}({heirClass} parameter)
    {{
    }}

    [{attribute.FullName}]
    public static void {attributedMethodName}()
    {{
    }}
}}
"
        };

        await workspace.AddSourceFileAsync(newFile, default);
        var compilation = await workspace.GetProjectCompilationAsync(projectName, default);

        // Act
        var attributedSymbols = (await SemanticsReceiver.AnalyzeAttributeBearersAsync(compilation, attribute.FullName.Yield()))
            .GetAttributeSymbols<ObsoleteAttribute>();

        // Assert
        var methods = attributedSymbols.OfType<IMethodSymbol>().Distinct().ToArray();
        var classes = attributedSymbols.OfType<INamedTypeSymbol>().Distinct().ToArray();

        Assert.AreEqual(2, methods.Length);
        Assert.AreEqual(2, classes.Length);

        Assert.True(methods.Any(m => m.Name == parameterMethodName));
        Assert.True(methods.Any(m => m.Name == attributedMethodName));
        Assert.True(classes.Any(c => c.Name == heirClass));
        Assert.True(classes.Any(c => c.Name == className));
    }


    [Test]
    public async Task Is_ForGeneric()
    {
        // Arrange
        using var workspace = CodeGenerationWorkspace.Create(_supplierProject);
        var projectName = workspace.ProjectLocations.Keys.First(_supplierProject.Contains);

        const string typeString = "System.Collections.Generic.List<Dictionary<int?, string?>>";

        await workspace.AddSourceFileAsync(projectName, "SymbolsSource.cs", $@"
    public class SymbolsSource
    {{
        public {typeString} List {{ get; set; }}
    }}
", default);

        var compilation = await workspace.GetProjectCompilationAsync(projectName, default);

        // Act
        var type = typeof(System.Collections.Generic.List<Dictionary<int?, string?>>);
        var listTypeSymbol = compilation.GetTypeByMetadataName("System.Collections.Generic.List`1")
             .Construct(compilation.GetTypeByMetadataName("System.Collections.Generic.Dictionary`2").Construct(
                    compilation.GetTypeByMetadataName("System.Nullable`1").Construct(compilation.GetTypeByMetadataName("System.Int32")),
                    compilation.GetTypeByMetadataName("System.Nullable`1").Construct(compilation.GetTypeByMetadataName("System.String"))
                 ));

        var typeDisplayString = type.ToDisplayString();

        var same = listTypeSymbol.Is(type);

        // Assert
        Assert.True(same);
    }

    static string[] Attributes = ("DestallMaterials.CodeGeneration.ERP.ClientDependency.TransportedAttribute", "DestallMaterials.CodeGeneration.ERP.ClientDependency.TargetDbContextAttribute").ToArray();
}
