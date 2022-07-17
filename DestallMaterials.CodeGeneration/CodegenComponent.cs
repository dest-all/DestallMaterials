using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;

namespace DestallMaterials.CodeGeneration
{
    public abstract class CodegenComponent : ComponentBase
    {

        [Parameter]
        public Compilation Compilation { get; set; }

        protected static readonly MarkupString GreaterSign = (MarkupString)">";
        protected static readonly MarkupString Arrow = (MarkupString)"=>";
        protected static readonly MarkupString G = GreaterSign;
        protected static readonly MarkupString L = (MarkupString)"<";
        protected static MarkupString Raw(string str) => (MarkupString)str;
        protected static MarkupString A => Arrow;
    }
}
