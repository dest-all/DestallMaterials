using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestComponents;

namespace DestallMaterials.Tests
{
    public class CodeGenerationTests
    {
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
