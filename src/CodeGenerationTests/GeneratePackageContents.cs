﻿using DestallMaterials.CodeGeneration;
using DestallMaterials.WheelProtection.Linq;


namespace CodeGenerationTests;

public class GeneratePackageContents
{
    [Test]
    public async Task GenerateLinqToTuples()
    {
        const byte maxTupleSize = 20;
        const string projectName = "DestallMaterials.WheelProtection.LinqToTuples";

        var parentFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent;

        var files = parentFolder.GetFiles();

        var linqToTuplesProject = Directory.GetFiles(parentFolder.FullName, $"*{projectName}.csproj", SearchOption.AllDirectories).First();

        using var workspace = CodeGenerationWorkspace.Create(linqToTuplesProject);

        var renderer = new SourceFileRenderer(workspace, sp => { });

        var filesAdded = await renderer.WriteToWorkspaceAsync<TestComponents.GeneratePackageContents>(
            workspace,
            (
                nameof(TestComponents.GeneratePackageContents.TupleSize), (object)maxTupleSize
            ).ToDictionary(),
            default);
    }
}