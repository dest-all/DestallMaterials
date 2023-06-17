using CodeGenerationExample.Generator.Lib;
using DestallMaterials.CodeGeneration;

var logger = new Logger();

var curDir = Directory.GetCurrentDirectory(); // where the executable resides.

var rootDirectory = Directory.GetParent(curDir).Parent.Parent.Parent.FullName;
// it's not mandatory to specify exactly the solution file directory.
// Renderer will perform a search in subdirectories and then parent directories, looking for project files needed.
// It will meet quicker success if the project files are at one folder distance from root path provided.

var renderer = new ProjectCodeRenderer(rootDirectory);

const string projectName = "CodeGenerationExample.Target"; // the project to get compilation object of

renderer.Bind<Template>(projectName); // i.e. the compilation of <projectName> will be passed to an instance of <Template> component as Compilation object
                                      // when you call renderer.RenderAsync. After that the template will be rendered immediately.

renderer.ConfigureRenderer((_, ren) => 
{
    ren.AddService(logger);
});

await renderer.RenderAsync(); // Obtaining the compilation object takes around 10 seconds - even for a tiny project.
                              // Consider launching parallel renders if you are to render code for several projects.

return 0;