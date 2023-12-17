using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using DestallMaterials.WheelProtection.Extensions.Tuples;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using DestallMaterials.WheelProtection.Extensions.Ranges;
using DestallMaterials.CodeGeneration;
using DestallMaterials.WheelProtection.Extensions.Objects;

namespace CodeGenerationTests;

[Parallelizable(scope: ParallelScope.All)]
[TestFixture]
public class CodegenSystemTests
{
    static readonly string _supplierProject;
    static readonly string _consumerProject;

    static CodegenSystemTests()
    {
        (_supplierProject, _consumerProject) = CodegenPreparation.EnsureSamples();
    }

    const string nameSpace = "GeneratedNamespace";
    const string className = "GeneratedClass";
    const string fileName = $"{className}.cs";
    const string sourceText = $"namespace {nameSpace} {{ public class {className} {{}} }}";
    [Test]
    public async Task AddSource_MustAdd_TreeMustBeFound()
    {
        // Arrange
        var system = CodeGenerationSystem.Create(_consumerProject);
        var projectName = system.ProjectNames.First(pn => pn.Contains("Consumer"));

        // Act
        var sourceFile = new SourceFile(new ProjectRelativeFilePath(projectName, fileName), sourceText);
        await system.AddSourceFilesAsync(sourceFile.Yield());
        var compilation = await system.GetProjectCompilationAsync(projectName);

        // Assert
        var containsClass = compilation.GetTypeByMetadataName($"{nameSpace}.{className}") is not null;
        Assert.True(containsClass);
    }

    [Test]
    public async Task AddSource_ToSecondaryProject_MustBeFound_InMainCompilation()
    {
        // Arrange
        var system = CodeGenerationSystem.Create(_consumerProject);
        var mainProjectName = system.ProjectNames.First(pn => pn.Contains("Consumer"));
        var secondaryProjectName = system.ProjectNames.First(pn => pn.Contains("Supplier"));

        // Act
        var sourceFile = new SourceFile(new ProjectRelativeFilePath(secondaryProjectName, fileName), sourceText);
        await system.AddSourceFilesAsync(sourceFile.Yield());
        var compilation = await system.GetProjectCompilationAsync(mainProjectName);

        // Assert
        var containsClass = compilation.GetTypeByMetadataName($"{nameSpace}.{className}") is not null;
        Assert.True(containsClass);
    }

    [Test]
    public async Task SecondaryProjectClass_MustBeFound_InMainProject()
    {
        // Arrange
        var system = CodeGenerationSystem.Create(_consumerProject);
        var mainProjectName = system.ProjectNames.First(pn => pn.Contains("Consumer"));
        var secondaryProjectName = system.ProjectNames.First(pn => pn.Contains("Supplier"));

        // Act
        var (mainCompilation, secondaryCompilation) = await (
            system.GetProjectCompilationAsync(mainProjectName),
            system.GetProjectCompilationAsync(secondaryProjectName));

        string classToFind = $"{secondaryProjectName}.SupplierModel";

        // Assert
        var secondaryContains = secondaryCompilation.GetTypeByMetadataName(classToFind) is not null;
        var mainContains = mainCompilation.GetTypeByMetadataName(classToFind) is not null;

        Assert.True(secondaryContains);
        Assert.True(mainContains);
    }

    [Test]
    public async Task Subscribe_SubscriptionMustWorkOnce()
    {
        // Arrange
        using var system = CodeGenerationSystem.Create(_consumerProject);
        var mainProjectName = system.ProjectNames.First(pn => pn.Contains("Consumer"));
        var secondaryProjectName = system.ProjectNames.First(pn => pn.Contains("Supplier"));

        // Act
        system.SubscribeForProjectChange(secondaryProjectName, async change =>
        {
            var (_, content, _) = change.Single();

            var classesCount = content.Split(" class ").Length - 1;
            await system.AddSourceFileAsync(mainProjectName, fileName, $@"namespace {nameSpace} 
{{
    public class {className}
    {{
        public const int ClassesCount = {classesCount};
    }}
}}");
        });

        const int newClassesCount = 3;

        await system.AddSourceFileAsync(secondaryProjectName, "newclasses.cs", $@"namespace {nameSpace} 
{{
    {(0..newClassesCount).Select(i => $"public class Class{i} {{}}").Join("\n")}
}}");

        var mainCompilation = await system.GetProjectCompilationAsync(mainProjectName);

        // Assert
        var classesCounterClass = mainCompilation.GetTypeByMetadataName($"{nameSpace}.{className}");

        Assert.IsNotNull(classesCounterClass);

        var classesCount = (int)classesCounterClass.GetMembers().OfType<IFieldSymbol>().First().ConstantValue!;

        Assert.AreEqual(newClassesCount, classesCount);
    }

    [Test]
    public async Task Subscribe_IncrementalSubscriptionMustWork()
    {
        // Arrange
        using var system = CodeGenerationSystem.Create(_consumerProject);
        var mainProjectName = system.ProjectNames.First(pn => pn.Contains("Consumer"));
        var secondaryProjectName = system.ProjectNames.First(pn => pn.Contains("Supplier"));

        const string structName = "NewStruct";

        // Act
        system.SubscribeForProjectChange(secondaryProjectName, async change =>
        {
            var (_, content, _) = change.Single();

            var classesCount = content.Split(" class ").Length - 1;
            if (classesCount > 0)
            {
                await system.AddSourceFileAsync(mainProjectName, fileName, $@"namespace {nameSpace} 
{{
    public class {className}
    {{
        public const int ClassesCount = {classesCount};
    }}
}}");
            }
        });

        system.SubscribeForProjectChange(mainProjectName, async change =>
        {
            var (_, content, _) = change.Single();

            var mainCompilation = await system.GetProjectCompilationAsync(mainProjectName);

            await system.AddSourceFileAsync(secondaryProjectName, "newstruct.cs", $@"namespace {nameSpace} 
{{
    public struct {structName}
    {{
    }}
}}");
        });

        const int newClassesCount = 3;

        await system.AddSourceFileAsync(secondaryProjectName, "newclasses.cs", $@"namespace {nameSpace} 
{{
    {(0..newClassesCount).Select(i => $"public class Class{i} {{}}").Join("\n")}
}}");

        var (mainCompilation, secondaryCompilation) = await (
            system.GetProjectCompilationAsync(mainProjectName), 
            system.GetProjectCompilationAsync(secondaryProjectName));

        // Assert
        var newStruct = secondaryCompilation.GetTypeByMetadataName($"{nameSpace}.{structName}");
        Assert.IsNotNull(newStruct);
    }

    [Test]
    public async Task RunIntegrated()
    {
        var ticker = Stopwatch.StartNew();

        var system = CodeGenerationSystem.Create(_consumerProject);

        const string mainProjectName = "CodegenSample.Consumer";

        var mainCompilation1 = await system.GetProjectCompilationAsync(mainProjectName);

        var errors1 = GetErrors(mainCompilation1);

        var referredProjects = system.ProjectNames.Where(pn => pn != mainProjectName).ToArray();

        var newClasses = referredProjects.Select(project =>
        {
            var name = project;

            var addedClass = name.Split('.')[^1] + "Class";
            var newClassCode = $@"namespace {name}; public class {addedClass} {{}}";

            addedClass = name + "." + addedClass;

            return (project, addedClass, newClassCode);
        }).ToArray();

        var newClassCode = $@"public class NewClass 
{{
   {newClasses.Select((nc, i) => $"public {nc.addedClass} Prop_{i} {{ get; set; }}").Join("\n")} 
}}";

        await system.AddSourceFileAsync(mainProjectName, "newClass.cs", newClassCode);

        var mainCompilation2 = await system.GetProjectCompilationAsync(mainProjectName);

        var errors2 = GetErrors(mainCompilation2);

        var newFiles = newClasses.Select(nc => new SourceFile(new ProjectRelativeFilePath(nc.project, "newclass.cs"), nc.newClassCode));

        await system.AddSourceFilesAsync(newFiles);

        var syntaxTrees = await referredProjects.ToArrayAsync(rp => system.GetProjectCompilationAsync(rp).Then(c => c.SyntaxTrees.ToArray()));

        var mainCompilation3 = await system.GetProjectCompilationAsync(mainProjectName);

        var errors3 = GetErrors(mainCompilation3);

        var mainSyntaxTree = mainCompilation3.SyntaxTrees;

        Assert.IsFalse(errors1.Any());
        Assert.IsTrue(errors2.Any());
        Assert.IsFalse(errors3.Any());
    }

    static Diagnostic[] GetErrors(Compilation compilation)
        => compilation
            .GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ToArray();

    static bool HasErrors(Compilation compilation) => GetErrors(compilation).Any();
}