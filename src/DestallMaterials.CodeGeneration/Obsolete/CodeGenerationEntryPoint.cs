using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;

namespace DestallMaterials.CodeGeneration
{
    public abstract class CodeGenerationEntryPoint : ComponentBase
    {
        public static class SourceCodeRenderingSymbols
        {
            public static readonly MarkupString GreaterSign = (MarkupString)">";
            public static readonly MarkupString Arrow = (MarkupString)"=>";
            public static readonly MarkupString G = GreaterSign;
            public static readonly MarkupString L = (MarkupString)"<";
            public static MarkupString Raw(string str) => (MarkupString)str;
            public static MarkupString A => Arrow;
        }

        [Parameter]
        [EditorRequired]
        public Compilation Compilation { get; set; }
    }
}
