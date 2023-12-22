using Microsoft.CodeAnalysis;

namespace DestallMaterials.CodeGeneration;

public static class CodeFilesAddingExtensions
{
    public static async Task<IReadOnlyList<CodeFile>> RenderToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        string projectName,
        CodeGenerationWorkspace codeGenerationSystem,
        IEnumerable<KeyValuePair<string, object>> parameters,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
    {
        var compilation = await codeGenerationSystem.GetProjectCompilationAsync(projectName);

        var sourceFiles = sourceFileRenderer.RenderSourceCode<TComponent>(compilation!, parameters);

        var addedSourceFiles = await codeGenerationSystem.AddSourceFilesAsync(sourceFiles, cancellationToken);

        return addedSourceFiles;
    }

    public static Task<IReadOnlyList<CodeFile>> RenderToWorkspaceAsync<TComponent>(
        this SourceFileRenderer sourceFileRenderer,
        string projectName,
        CodeGenerationWorkspace codeGenerationSystem,
        CancellationToken cancellationToken)
        where TComponent : SourceGenerationTemplate
        => sourceFileRenderer.RenderToWorkspaceAsync<TComponent>(
            projectName,
            codeGenerationSystem,
            Enumerable.Empty<KeyValuePair<string, object>>(),
            cancellationToken);

        /// <summary>
        /// Render source code template to Source code files.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="compilation"></param>
        /// <returns></returns>
        public static IEnumerable<CodeFile> RenderSourceCode<TComponent>(this SourceFileRenderer sourceFileRenderer, Compilation compilation)
        where TComponent : SourceGenerationTemplate
        => sourceFileRenderer.RenderSourceCode<TComponent>(compilation, Enumerable.Empty<KeyValuePair<string, object>>());
}