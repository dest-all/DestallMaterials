using BlazorTemplater;
using Buildalyzer;
using Buildalyzer.Workspaces;
using DestallMaterials.CodeGeneration.Environment;
using DestallMaterials.CodeGeneration.Text;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace DestallMaterials.CodeGeneration
{
    public class ProjectCodeRenderer
    {
        readonly Dictionary<string, Type> _entryTemplatesByProjects = new Dictionary<string, Type>();
        readonly SolutionPathFinder _pathFinder;
        readonly ConsoleLogger _logger = new ConsoleLogger();
        readonly FileWriter _fileWriter;
        Action<Compilation, ComponentRenderer<DynamicComponent>>? _rendererConfiguration;
        public ProjectCodeRenderer(string solutionDirectoryPath)
        {
            _pathFinder = SolutionPathFinder.Create(solutionDirectoryPath);
            _fileWriter = new FileWriter(_pathFinder, _logger);
        }


        /// <summary>
        /// Binds entry template to project.
        /// </summary>
        /// <typeparam name="TTemplate"></typeparam>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ProjectCodeRenderer Bind<TTemplate>(string projectName)
            where TTemplate : CodegenComponent
        {
            if (_entryTemplatesByProjects.Any(kv => kv.Key.Trim().ToLower() == projectName.ToLower()))
            {
                throw new InvalidOperationException($"Entry template already set for project {projectName}.");
            }
            if (_pathFinder.RelativeToAbsolutePath(projectName) == null)
            {
                throw new InvalidOperationException($"Could not find path to project with the name {projectName}.");
            }

            _entryTemplatesByProjects.Add(projectName, typeof(TTemplate));

            return this;
        }

        /// <summary>
        /// Binds entry template to project.
        /// </summary>
        /// <typeparam name="TTemplate"></typeparam>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ProjectCodeRenderer Bind(string projectName, Type templateType)
        {
            if (_entryTemplatesByProjects.Any(kv => kv.Key.Trim().ToLower() == projectName.ToLower()))
            {
                throw new InvalidOperationException($"Entry template already set for project {projectName}.");
            }
            if (_pathFinder.RelativeToAbsolutePath(projectName) == null)
            {
                throw new InvalidOperationException($"Could not find path to project with the name {projectName}.");
            }

            if (!IsComponent(templateType))
            {
                throw new Exception($"Template component provided is not a {typeof(CodegenComponent).FullName}.");
            }

            _entryTemplatesByProjects.Add(projectName, templateType);

            return this;
        }

        ComponentRenderer<Microsoft.AspNetCore.Components.DynamicComponent> CreateComponentRenderer(Type componentType, Compilation compilation)
        {
            var renderer = new BlazorTemplater.ComponentRenderer<Microsoft.AspNetCore.Components.DynamicComponent>();

            renderer.Set(c => c.Type, componentType);
            renderer.Set(c => c.Parameters, new Dictionary<string, object>
            {
                { "Compilation", compilation }
            });

            if (_rendererConfiguration is not null)
            {
                _rendererConfiguration(compilation, renderer);
            }

            return renderer;
        }

        public void ConfigureRenderer(Action<Compilation, ComponentRenderer<DynamicComponent>> configuringAction)
        {
            _rendererConfiguration = configuringAction;
        }

        async Task<Compilation> GetProjectCompilationAsync(string projectName)
        {
            string projectToAnalyze = Path.Combine(_pathFinder.RelativeToAbsolutePath(projectName), projectName)
                .MustEndWith(".csproj");

            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(projectToAnalyze);

            var buildResultsTask = Task.Run(() => analyzer.Build());

            var solution = analyzer.SolutionDirectory;

            var workspace = new AdhocWorkspace();

            var buildResult = await buildResultsTask;
            var dependencyProjects = buildResult.First().ProjectReferences.SelectAsync(r => Task.Run(() => manager.GetProject(r)));

            await foreach (var dependency in dependencyProjects)
            {
                dependency.AddToWorkspace(workspace);
            }

            var project = analyzer.AddToWorkspace(workspace);

            var compilation = await project.GetCompilationAsync();

            return compilation ?? throw new InvalidOperationException($"Could not create compilation data object of project {projectName}.");
        }

        public async Task<SourceFileData[]> RenderAsync(bool writeFiles = true)
        {
            List<string> rawResults = new List<string>();

            try
            {
                foreach (var (projectName, templateToRender) in _entryTemplatesByProjects)
                {
                    var compilation = await GetProjectCompilationAsync(projectName);

                    var renderer = CreateComponentRenderer(templateToRender, compilation);

                    var subresult = renderer.Render();

                    rawResults.Add(subresult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during rendering. {ex.Message}\n {ex.StackTrace}.", ex);
                return new SourceFileData[0];
            }

            var sourceFiles = rawResults.SelectMany(content => _fileWriter.SplitIntoFiles(content)).ToArray();

            if (writeFiles)
            {
                await _fileWriter.WriteSourceFilesAsync(sourceFiles);
            }

            return sourceFiles;
        }

        static bool IsComponent(Type type)
        {
            type = type.BaseType;
            while (type is not null)
            {
                if (type == typeof(CodegenComponent))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}
