using DestallMaterials.WheelProtection.Extensions.Strings;

namespace DestallMaterials.CodeGeneration;

public static class CodeFileExtensions
{
    public static bool IsCharpFile(this CodeFile codeFile)
        => codeFile.Path.FileName.ToLower().EndsWith(".cs", ".razor");

}