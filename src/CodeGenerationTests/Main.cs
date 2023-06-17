using DestallMaterials.CodeGeneration.Text;
using HtmlAgilityPack;
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

        [Test]
        public void ParseCodePieceIntoFileCorrectly()
        {
            const string input = @"<File Path=""Client.Web.View/Autogenerated/Components/Models/Entities/ReferrableEntities/Documents/Recruitment/PropertyInputs/Number.razor""><MudTextField @@bind-Value=""Item.Number"" Label=""Number"" Disabled=""Disabled""></MudTextField><text>@code {
            [Parameter]
            public bool Disabled { get; set; }

            [Parameter]
            [EditorRequired]
            public Protocol.Models.Entities.ReferrableEntities.Documents.Recruitment.IRecruitmentModel Item { get; set; }
        }
    </text></File>";

            var result = FileWriter._outpuFilesParser.Matches(input)[0];

            var content = result.Groups["Content"];
            var path = result.Groups["Path"];

        }
    }
}