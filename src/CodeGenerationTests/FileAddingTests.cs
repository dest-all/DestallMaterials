using DestallMaterials.CodeGeneration;
using DestallMaterials.WheelProtection.Extensions.Objects;
using DestallMaterials.WheelProtection.Linq;

namespace CodeGenerationTests;

[Parallelizable(scope: ParallelScope.All)]
[TestFixture]
public class FileAddingTests : CodeGenerationTests
{
    [Test]
    public async Task AddFile_Existing_ShouldNotAdd()
    {
        // Arrange
        var system = CodeGenerationWorkspace.Create(_consumerProject);
        var projectName = system.ProjectLocations.Keys.First(pn => pn.Contains("Consumer"));
        var folders = "FolderedItems".ToArrayOfOne();
        
        // Act
        var sourceFile = new CodeFile(new ProjectRelativeFilePath(projectName, folders, "FolderedClass.cs"), sourceText);
        var added = await system.AddSourceFilesAsync(sourceFile.Yield(), default);
        
        // Assert
        Assert.IsEmpty(added);
    }

    [Test]
    public async Task AddFile_New_ShouldAdd()
    {
        // Arrange
        var system = CodeGenerationWorkspace.Create(_consumerProject);
        var projectName = system.ProjectLocations.Keys.First(pn => pn.Contains("Consumer"));

        // Act
        var sourceFile = new CodeFile(new ProjectRelativeFilePath(projectName, "ConsumerModel2.cs"), sourceText);
        var added = await system.AddSourceFilesAsync(sourceFile.Yield(), default);

        // Assert
        Assert.IsNotEmpty(added);
    }

    [Test]
    public async Task AddFile_New_NextDifferent_ShouldAdd_AndOverwrite()
    {
        // Arrange
        var system = CodeGenerationWorkspace.Create(_consumerProject);
        var projectName = system.ProjectLocations.Keys.First(pn => pn.Contains("Consumer"));
        string[] folders = ("Folder1", "Folder2", "LastFolder").ToArray();

        // Act
        var sourceFile = new CodeFile(new ProjectRelativeFilePath(projectName, folders, "ConsumerModel2.cs"), sourceText);
        var added1 = await system.AddSourceFileAsync(sourceFile, default);
        var added2 = await system.AddSourceFileAsync(sourceFile, default);
        var added3 = await system.AddSourceFileAsync(sourceFile with { Content = sourceText + sourceText }, default);

        // Assert
        Assert.IsTrue(added1);
        Assert.IsFalse(added2);
        Assert.IsFalse(added3);
    }
}