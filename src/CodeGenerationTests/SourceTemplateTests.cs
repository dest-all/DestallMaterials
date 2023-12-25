using DestallMaterials.CodeGeneration;
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