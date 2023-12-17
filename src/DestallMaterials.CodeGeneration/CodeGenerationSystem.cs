using Buildalyzer;
using Buildalyzer.Workspaces;
using DestallMaterials.WheelProtection.Extensions.Tuples;
using Microsoft.CodeAnalysis;
using DestallMaterials.WheelProtection.Extensions.Objects;
using DestallMaterials.WheelProtection.Extensions.Tasks;

namespace DestallMaterials.CodeGeneration;

public sealed class CodeGenerationSystem : IDisposable
{
    volatile Solution _solution;

    readonly List<Func<IReadOnlyList<SourceFile>, Task>> _onProjectsChange = new();

    static void Log(object message)
        => Console.WriteLine(message);

    public IReadOnlyList<string> ProjectNames { get; }

    CodeGenerationSystem(Solution solution)
    {
        _solution = solution;
        ProjectNames = solution.Projects.Select(p => p.Name).ToArray();
    }

    public static CodeGenerationSystem Create(string mainProjectFile)
    {
        var workspace = CreateWorkspace(mainProjectFile);

        var solution = workspace.CurrentSolution;

        return new CodeGenerationSystem(solution);
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
    public async Task<IReadOnlyList<SourceFile>> AddSourceFilesAsync(IEnumerable<SourceFile> sourceFiles)
    {
        var processedProjects = await Task.WhenAll(sourceFiles.Select(async sourceFile =>
        {
            var oldProject = _solution.Projects.First(p => p.Name == sourceFile.Path.ProjectName);
            var newProject = await oldProject.WithAsync(sourceFile);

            bool areDifferent = !ReferenceEquals(newProject, oldProject);

            _solution = newProject.Solution;

            return (oldProject, newProject, sourceFile, areDifferent);
        }));

        var changedProjects = processedProjects
            .Where(pp => pp.areDifferent).ToArray();

        var addedFiles = changedProjects.Select(cp => cp.sourceFile).ToArray();

        foreach (var callback in _onProjectsChange)
        {
            await callback(addedFiles);
        }

        return addedFiles;
    }

    /// <summary>
    /// Subscribe for the case any project gets added source files to.
    /// </summary>
    /// <param name="onProjectDocumentChanged">Callback</param>
    /// <returns>Unsubsription token. Dispose to unsubscribe.</returns>
    public IDisposable SubscribeForSolutionChange(Func<IReadOnlyList<SourceFile>, Task> onProjectDocumentChanged)
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

public static class CodegenSystemExtensions
{
    /// <summary>
    /// Subscribe for particular project changes
    /// </summary>
    /// <param name="codegenSystem">Code generation system</param>
    /// <param name="projectName">Project to whose changes to subscribe</param>
    /// <param name="callback">Callback</param>
    /// <returns>Unsibscription token</returns>
    public static IDisposable SubscribeForProjectChange(this CodeGenerationSystem codegenSystem, string projectName, Func<IReadOnlyList<SourceFile>, Task> callback)
        => codegenSystem.SubscribeForSolutionChange(changes =>
        {
            var relevantChanges = changes.Where(c => c.Path.ProjectName == projectName).ToArray();
            if (relevantChanges.Length == 0)
            {
                return Task.CompletedTask;
            }
            return callback(relevantChanges);
        });

    /// <summary>
    /// Add one source file to system.
    /// </summary>
    /// <param name="codeGenerationSystem">System</param>
    /// <param name="sourceFile">File to add</param>
    /// <returns>Whether the file has been added or not</returns>
    public static Task<bool> AddSourceFileAsync(this CodeGenerationSystem codeGenerationSystem, SourceFile sourceFile)
        => codeGenerationSystem.AddSourceFilesAsync(sourceFile.Yield()).Then(files => files.Any());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codeGenerationSystem"></param>
    /// <param name="projectName"></param>
    /// <param name="fileName"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Task<bool> AddSourceFileAsync(
        this CodeGenerationSystem codeGenerationSystem,
        string projectName,
        string fileName,
        string code)
        => codeGenerationSystem.AddSourceFileAsync(new SourceFile(new ProjectRelativeFilePath(projectName, fileName), code));
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
