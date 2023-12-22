using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;

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

        return Create(solution);
    }

    public static CodeGenerationWorkspace Create(Solution solution)
        => new(solution);

    public static CodeGenerationWorkspace Create(Workspace workspace)
        => Create(workspace.CurrentSolution);

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
