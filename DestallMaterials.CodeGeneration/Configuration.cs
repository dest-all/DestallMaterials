using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.CodeGeneration
{
    class CodegenSettings
    {
        public List<ProjectCodeGenerationSettings> projectSettings { get; set; } = new List<ProjectCodeGenerationSettings>();
        public Dictionary<string, string> extensionLineCommentPatterns { get; set; } = new Dictionary<string, string>
        {
            { "razor", "@*{0}*@" },
            { "cshtml", "@*{0}*@" },
            { "pas", "// {0}" },
            { "c", "// {0}" },
            { "cpp", "// {0}" },
            { "cs", "// {0}" },
            { "bat", "rem {0}" },
            { "sql", "-- {0}" },
            { "js", "// {0}"},
            { "ts", "// {0}"},
            { "tsx", "// {0}"},
            { "jsx", "// {0}"}
        };

        public IEnumerable<string> allowedApplicationDomains = new[]
        {
            "VBCSCompiler.exe"
        };

        public bool Log { get; set; } = true;

        public string CodeGeneratorServerBaseUrl { get; set; }
        public string CodeGeneratorUniversalMethodAddress { get; set; }
    }
    class ProjectCodeGenerationSettings
    {
        public string ProjectName { get; set; }
        public bool RunCodeGeneration { get; set; } = false;
        public bool DebugMode { get; set; } = false;
        public bool IncludeGeneratedToProject { get; set; } = false;
        public bool Log { get; set; } = true;
        public string GeneratedFileSuffix { get; set; } = "";

    }

}
