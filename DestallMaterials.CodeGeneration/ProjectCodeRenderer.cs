using BlazorTemplater;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DestallMaterials.CodeGeneration
{
    public class ProjectCodeRenderer
    {
        readonly Dictionary<string, Type> _entryTemplatesByProjects = new Dictionary<string, Type>();
        readonly SolutionPathFinder _pathFinder;
        Action<Compilation, ComponentRenderer<DynamicComponent>> _rendererConfiguration;
        public ProjectCodeRenderer(string solutionDirectoryPath)
        {
            _pathFinder = SolutionPathFinder.Create(solutionDirectoryPath);
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
            string projectToAnalyze = Path.Combine(_pathFinder.RelativeToAbsolutePath(projectName), projectName);

            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(projectToAnalyze);
            var workspace = new AdhocWorkspace();

            var project = analyzer.AddToWorkspace(workspace);

            var compilation = await project.GetCompilationAsync();

            return compilation ?? throw new InvalidOperationException($"Could not create compilation data object of project {projectName}.");
        }

        public async Task RenderAsync()
        { 

        }
    }
}
