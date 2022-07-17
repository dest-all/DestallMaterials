using TestComponents;

namespace CodeGenerationTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        private const string ProjectName = "DestallMaterials.Tests";

        [Test]
        public async Task GenerateSimpleFile()
        {
            const string basePath = @"C:\Users\igor_\source\repos\DestallPackages";

            var renderer = new DestallMaterials.CodeGeneration.ProjectCodeRenderer(basePath);

            renderer.Bind<TestCodegenComponent>(ProjectName);

            var result = await renderer.RenderAsync(false);
        }
    }
}