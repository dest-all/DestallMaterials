using Microsoft.CodeAnalysis;

namespace DestallMaterials.CodeGeneration;

public static class CodeFilesAddingExtensions
{
    /// <summary>
    /// Render templated code and include it into the workspace. Doesn't create physical files.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="parameters">Parameters to pass to the rendered template</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static async Task<IReadOnlyList<CodeFile>> RenderToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        CodeGenerationWorkspace codeGenerationWorkspace,
        IEnumerable<KeyValuePair<string, object>> parameters,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
    {
        var sourceFiles = sourceFileRenderer.RenderSourceCode<TComponent>(parameters);

        var addedSourceFiles = await codeGenerationWorkspace.AddSourceFilesAsync(sourceFiles, cancellationToken);

        return addedSourceFiles;
    }

    /// <summary>
    /// Render templated code and include it into the workspace. Creates physical source files, according to the template.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="parameters">Parameters to pass to the rendered template</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static async Task<IReadOnlyList<string>> WriteToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        CodeGenerationWorkspace codeGenerationWorkspace,
        IEnumerable<KeyValuePair<string, object>> parameters,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
    {
        var addedFiles = await sourceFileRenderer.RenderToWorkspaceAsync<TComponent>(codeGenerationWorkspace, parameters, cancellationToken);
        var result = await codeGenerationWorkspace.WriteFilesAsync(addedFiles, cancellationToken);
        return result;
    }

    /// <summary>
    /// Render templated code and include it into the workspace. Creates physical source files, according to the template.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static Task<IReadOnlyList<string>> WriteToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        CodeGenerationWorkspace codeGenerationWorkspace,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate => sourceFileRenderer.WriteToWorkspaceAsync<TComponent>(
            codeGenerationWorkspace,
            Enumerable.Empty<KeyValuePair<string, object>>(),
            cancellationToken);

    /// <summary>
    /// Render templated code and include it into the workspace with no parameters to pass to the template. Doesn't create physical files.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <param name="sourceFileRenderer">Preconfigured source code templates renderer</param>
    /// <param name="codeGenerationWorkspace">Workspace to put rendered code to</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Files added to the workspace, that haven't been present before</returns>
    public static Task<IReadOnlyList<CodeFile>> RenderToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        CodeGenerationWorkspace codeGenerationWorkspace,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
            => sourceFileRenderer.RenderToWorkspaceAsync<TComponent>(
                    codeGenerationWorkspace,
                    Enumerable.Empty<KeyValuePair<string, object>>(),
                    cancellationToken);

    /// <summary>
    /// Render source code template to Source code files.
    /// </summary>
    /// <typeparam name="TComponent">Component to use as source code rendering template</typeparam>
    /// <returns></returns>
    public static IEnumerable<CodeFile> RenderSourceCode<TComponent>(this SourceFileRenderer sourceFileRenderer)
        where TComponent : SourceGenerationTemplate
        => sourceFileRenderer.RenderSourceCode<TComponent>(Enumerable.Empty<KeyValuePair<string, object>>());

    static IEnumerable<string> GetFoldersHierarchy(Document document)
    {
        if (document.FilePath is null)
        {
            return document.Folders;
        }

        var projectDirectoryLocation = Directory.GetParent(document.Project.FilePath).FullName;

        if (!document.FilePath.StartsWith(projectDirectoryLocation))
        {
            throw new SourceCodeGenerationException("Document is not located at project folder.");
        }

        var pastPath = document.FilePath[(projectDirectoryLocation.Length + 1)..];

        var result = pastPath.Split(Path.DirectorySeparatorChar)[..^1];

        return result;
    }

    /// <summary>
    /// Find document with the presented <see cref="relativePath" />
    /// </summary>
    /// <param name="documents">Sequence of documents to seek in.</param>
    /// <param name="relativePath">Path</param>
    /// <returns></returns>
    public static Document? Find(this IEnumerable<Document> documents, ProjectRelativeFilePath relativePath)
        => documents.FirstOrDefault(d => d.Project.Name == relativePath.ProjectName && d.Name == relativePath.FileName && GetFoldersHierarchy(d).SequenceEqual(relativePath.Folders));
}