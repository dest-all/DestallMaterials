using DestallMaterials.WheelProtection.Extensions.Strings;
using Microsoft.CodeAnalysis;

namespace CodeGenerationTests;

public record struct SourceFilePath(string ProjectName, IEnumerable<string> Folders, string FileName)
{
    public override string ToString()
        => Folders.Any() ? $"{ProjectName}/{Folders.Join("/")}/{FileName}" :
                           $"{ProjectName}/{FileName}";

    public static SourceFilePath Parse(string relativePathAsString)
    {
        var splitted = relativePathAsString.Split('/');

        if (splitted.Length < 2)
        {
            throw new ArgumentException("Unparsable SourceFilePath string.");
        }

        var projectName = splitted.First();
        var fileName = splitted.Last();
        var folders = splitted[1..^2];

        return new(projectName, folders, fileName);
    }
}
