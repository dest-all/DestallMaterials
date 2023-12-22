using Microsoft.CodeAnalysis;

namespace DestallMaterials.CodeGeneration;

public static class CodeFilesAddingExtensions
{
    /// <summary>
    /// Render templated code and include it into the workspace. Doesn't create physical files.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="projectName">Project to get compilation object of to pass into the template</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="parameters">Parameters to pass to the rendered template</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static async Task<IReadOnlyList<CodeFile>> RenderToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        string projectName,
        CodeGenerationWorkspace codeGenerationWorkspace,
        IEnumerable<KeyValuePair<string, object>> parameters,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
    {
        var compilation = await codeGenerationWorkspace.GetProjectCompilationAsync(projectName);

        var sourceFiles = sourceFileRenderer.RenderSourceCode<TComponent>(compilation!, parameters);

        var addedSourceFiles = await codeGenerationWorkspace.AddSourceFilesAsync(sourceFiles, cancellationToken);

        return addedSourceFiles;
    }

    /// <summary>
    /// Render templated code and include it into the workspace. Creates physical source files, according to the template.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="projectName">Project to get compilation object of to pass into the template</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="parameters">Parameters to pass to the rendered template</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static async Task<IReadOnlyList<CodeFile>> WriteToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        string projectName,
        CodeGenerationWorkspace codeGenerationWorkspace,
        IEnumerable<KeyValuePair<string, object>> parameters,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
    {
        var addedFiles = await sourceFileRenderer.RenderToWorkspaceAsync<TComponent>(projectName, codeGenerationWorkspace, parameters, cancellationToken);
        await codeGenerationWorkspace.WriteFilesAsync(addedFiles, cancellationToken);
        return addedFiles;
    }

    /// <summary>
    /// Render templated code and include it into the workspace. Creates physical source files, according to the template.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="projectName">Project to get compilation object of to pass into the template</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static Task<IReadOnlyList<CodeFile>> WriteToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        string projectName,
        CodeGenerationWorkspace codeGenerationWorkspace,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate => sourceFileRenderer.WriteToWorkspaceAsync<TComponent>(
            projectName,
            codeGenerationWorkspace,
            Enumerable.Empty<KeyValuePair<string, object>>(),
            cancellationToken);

    /// <summary>
    /// Render templated code and include it into the workspace with no parameters to pass to the template. Doesn't create physical files.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="projectName">Project to get compilation object of to pass into the template</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static Task<IReadOnlyList<CodeFile>> RenderToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        string projectName,
        CodeGenerationWorkspace codeGenerationWorkspace,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
            => sourceFileRenderer.RenderToWorkspaceAsync<TComponent>(
                    projectName,
                    codeGenerationWorkspace,
                    Enumerable.Empty<KeyValuePair<string, object>>(),
                    cancellationToken);

    /// <summary>
    /// Render source code template to Source code files.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="compilation">COmpilation object to pass to the specified template</param>
    /// <returns></returns>
    public static IEnumerable<CodeFile> RenderSourceCode<TComponent>(this SourceFileRenderer sourceFileRenderer, Compilation compilation)
        where TComponent : SourceGenerationTemplate
        => sourceFileRenderer.RenderSourceCode<TComponent>(compilation, Enumerable.Empty<KeyValuePair<string, object>>());
}