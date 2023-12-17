using Buildalyzer;
using Buildalyzer.Workspaces;
using DestallMaterials.WheelProtection.Extensions.Tuples;
using Microsoft.CodeAnalysis;
using DestallMaterials.WheelProtection.Extensions.Objects;
using DestallMaterials.WheelProtection.Extensions.Tasks;

namespace DestallMaterials.CodeGeneration;

public sealed class CodeGenerationWorkspace : IDisposable
{
    volatile Solution _solution;

    readonly List<Func<IReadOnlyList<CodeFile>, CancellationToken, Task>> _onProjectsChange = new();

    public IReadOnlyDictionary<string, string> ProjectLocations { get; }

    CodeGenerationWorkspace(Solution solution)
    {
        _solution = solution;
        ProjectLocations = solution.Projects.ToDictionary(p => p.Name, p => p?.FilePath ?? throw new FileNotFoundException());
    }

    public static CodeGenerationWorkspace Create(string mainProjectFile)
    {
        var workspace = CreateWorkspace(mainProjectFile);

        var solution = workspace.CurrentSolution;

        return new CodeGenerationWorkspace(solution);
    }

    public async Task<Compilation> GetProjectCompilationAsync(string projectName, CancellationToken cancellationToken = default)
    {
        var project = _solution.Projects.First(p => p.Name == projectName);

        var result = await project.GetCompilationAsync(cancellationToken);

        return result!;
    }

    /// <summary>
    /// Added a bunch of source files to system.
    /// </summary>
    /// <param name="sourceFiles">Files to add</param>
    /// <returns>Those files, that were not present and have been added to system.</returns>
    public async Task<IReadOnlyList<CodeFile>> AddSourceFilesAsync(IEnumerable<CodeFile> sourceFiles, CancellationToken cancellationToken)
    {
        var processedProjects = await Task.WhenAll(sourceFiles.Select(async sourceFile =>
        {
            var oldProject = _solution.Projects.First(p => p.Name == sourceFile.Path.ProjectName);
            if (!sourceFile.IsCharpFile())
            {
                return (oldProject, newProject: oldProject, sourceFile, areDifferent: false);
            }
            
            var newProject = await oldProject.WithAsync(sourceFile, cancellationToken);

            bool areDifferent = !ReferenceEquals(newProject, oldProject);

            _solution = newProject.Solution;

            return (oldProject, newProject, sourceFile, areDifferent);
        }));

        var changedProjects = processedProjects
            .Where(pp => pp.areDifferent).ToArray();

        var addedFiles = changedProjects.Select(cp => cp.sourceFile).ToArray();

        foreach (var callback in _onProjectsChange)
        {
            await callback(addedFiles, cancellationToken);
        }

        return addedFiles;
    }

    /// <summary>
    /// Subscribe for the case any project gets added source files to.
    /// </summary>
    /// <param name="onProjectDocumentChanged">Callback</param>
    /// <returns>Unsubsription token. Dispose to unsubscribe.</returns>
    public IDisposable SubscribeForSolutionChange(Func<IReadOnlyList<CodeFile>, CancellationToken, Task> onProjectDocumentChanged)
    {
        _onProjectsChange.Add(onProjectDocumentChanged);
        DisposableCallback unsubcribe = new(() => _onProjectsChange.Remove(onProjectDocumentChanged));
        return unsubcribe;
    }

    static AdhocWorkspace CreateWorkspace(string csprojFile)
    {
        string projectToAnalyze = csprojFile;

        if (!File.Exists(projectToAnalyze))
        {
            throw new FileNotFoundException(projectToAnalyze);
        }

        AnalyzerManager manager = new AnalyzerManager();
        IProjectAnalyzer analyzer = manager.GetProject(projectToAnalyze);

        var workspace = new AdhocWorkspace();

        analyzer.AddToWorkspace(workspace, true);

        return workspace;
    }

    public void Dispose()
        => _onProjectsChange.Clear();
}

public static class CodegenWorkspaceExtensions
{
    /// <summary>
    /// Subscribe for particular project changes
    /// </summary>
    /// <param name="codegenSystem">Code generation system</param>
    /// <param name="projectName">Project to whose changes to subscribe</param>
    /// <param name="callback">Callback</param>
    /// <returns>Unsibscription token</returns>
    public static IDisposable SubscribeForProjectChange(this CodeGenerationWorkspace codegenSystem, string projectName, Func<IReadOnlyList<CodeFile>, CancellationToken, Task> callback)
        => codegenSystem.SubscribeForSolutionChange((changes, ct) =>
        {
            var relevantChanges = changes.Where(c => c.Path.ProjectName == projectName).ToArray();
            if (relevantChanges.Length == 0)
            {
                return Task.CompletedTask;
            }
            return callback(relevantChanges, ct);
        });

    /// <summary>
    /// Add one source file to system.
    /// </summary>
    /// <param name="codeGenerationSystem">System</param>
    /// <param name="sourceFile">File to add</param>
    /// <returns>Whether the file has been added or not</returns>
    public static Task<bool> AddSourceFileAsync(this CodeGenerationWorkspace codeGenerationSystem, CodeFile sourceFile, CancellationToken cancellationToken)
        => codeGenerationSystem.AddSourceFilesAsync(sourceFile.Yield(), cancellationToken).Then(files => files.Any());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codeGenerationWorkspace"></param>
    /// <param name="projectName"></param>
    /// <param name="fileName"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Task<bool> AddSourceFileAsync(
        this CodeGenerationWorkspace codeGenerationWorkspace,
        string projectName,
        string fileName,
        string code,
        CancellationToken cancellationToken)
        => codeGenerationWorkspace.AddSourceFileAsync(new CodeFile(new ProjectRelativeFilePath(projectName, fileName), code), cancellationToken);


    public static async Task WriteFilesAsync(this CodeGenerationWorkspace codeGenerationWorkspace, IEnumerable<CodeFile> sourceFiles, CancellationToken cancellationToken)
    {
        foreach (var sourceFile in sourceFiles)
        {
            if (sourceFile.Virtual)
            {
                continue;
            }
            await codeGenerationWorkspace.WriteAsync(sourceFile.WithAutogeneratedAnnotation(), cancellationToken);
        }
    }
}

file class DisposableCallback : IDisposable
{
    Action OnDisposed { get; }

    internal DisposableCallback(Action onDisposed)
    {
        OnDisposed = onDisposed;
    }

    public static implicit operator DisposableCallback(Action onDisposed)
        => new DisposableCallback(onDisposed);

    public void Dispose() => OnDisposed();
}
