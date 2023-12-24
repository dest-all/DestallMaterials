using DestallMaterials.CodeGeneration;
using DestallMaterials.CodeGeneration.Utilities;
using DestallMaterials.WheelProtection.Extensions.Objects;
using Microsoft.CodeAnalysis;
using TestComponents;


namespace CodeGenerationTests;

[Parallelizable(scope: ParallelScope.All)]
[TestFixture]
public class SourceTemplateTests : CodeGenerationTests
{
    [Test]
    public async Task RenderFile_WithContent()
    {
        // Arrange
        using var workspace = CodeGenerationWorkspace.Create(_consumerProject);
        var renderer = new SourceFileRenderer(sp => { });
        var mainProjectName = workspace.ProjectLocations.Keys.First(_consumerProject.Contains);

        var parameters = ToTestComponentParameters(GeneratedFile(mainProjectName));

        // Act
        var renderResult = await renderer.RenderToWorkspaceAsync<TestCodegenComponent>(mainProjectName, workspace, parameters, default);

        // Assert
        var compilation = await workspace.GetProjectCompilationAsync(mainProjectName, default);
        var createdClassSymbol = compilation.GetTypeByMetadataName($"{nameSpace}.{className}");
        
        Assert.IsNotNull(createdClassSymbol);
    }
}

[Parallelizable(scope: ParallelScope.All)]
public class UtilitiesTesting : CodeGenerationTests
{
    [Test]
    public async Task CollectSymbolsWithAttributes()
    {
        // Arrange
        using var workspace = CodeGenerationWorkspace.Create(_supplierProject);
        var renderer = new SourceFileRenderer(sp => { });
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
        var symbolsReceiver = new SemanticsReceiver(compilation, attribute.FullName.Yield());

        // Act
        await symbolsReceiver.VisitAllSyntaxTreesAsync();


        // Assert
        var attributedSymbols = symbolsReceiver.GetAttributeSymbols<ObsoleteAttribute>();
        var methods = attributedSymbols.OfType<IMethodSymbol>().Distinct().ToArray();
        var classes = attributedSymbols.OfType<INamedTypeSymbol>().Distinct().ToArray();

        Assert.AreEqual(2, methods.Length);
        Assert.AreEqual(2, classes.Length);

        Assert.True(methods.Any(m => m.Name == parameterMethodName));
        Assert.True(methods.Any(m => m.Name == attributedMethodName));
        Assert.True(classes.Any(c => c.Name == heirClass));
        Assert.True(classes.Any(c => c.Name == className));
    }
}
