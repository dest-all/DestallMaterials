using DestallMaterials.WheelProtection.Extensions.Strings;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DestallMaterials.CodeGeneration;

/// <summary>
/// File 
/// </summary>
/// <param name="ProjectName"></param>
/// <param name="Folders"></param>
/// <param name="FileName"></param>
public record struct ProjectRelativeFilePath(string ProjectName, IEnumerable<string> Folders, string FileName)
{
    public ProjectRelativeFilePath(string projectName, string fileName) : this(projectName, Enumerable.Empty<string>(), fileName)
    { }

    public override string ToString() => ToString(Path.PathSeparator);
        
    string ToString(char separator) => Folders.Any() ? $"{ProjectName}{separator}{Folders.Join(separator)}{separator}{FileName}" :
                           $"{ProjectName}{separator}{FileName}";


    public static ProjectRelativeFilePath Parse(string relativePathAsString)
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

    [Obsolete("Must not be used without parameters", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ProjectRelativeFilePath() : this(default, default, default) { }
}

/// <summary>
/// Holds data about rendered source code.
/// </summary>
/// <param name="Path"></param>
/// <param name="Content"></param>
/// <param name="Virtual"></param>
public record struct CodeFile(ProjectRelativeFilePath Path, string Content, bool Virtual = false)
{
    static readonly Regex _sourceCodeFilesParser = new Regex(@"<(\s)*File(\s)*Path(\s)*=(\s)*""(?<Path>.*?)""(\s)*Virtual(\s)*=(\s)*""(?<Virtual>.*?)"".*?>(?<Content>(.|\s)*?)<(\s)*/(\s)*File(\s)*>", RegexOptions.Compiled);
    public static IEnumerable<CodeFile> ParseMany(string renderingResult)
    {
        var matches = _sourceCodeFilesParser.Matches(renderingResult);

        foreach (var fileFound in matches.AsEnumerable())
        {
            var path = ProjectRelativeFilePath.Parse(fileFound.Groups["Path"].Value);
            bool pureVirtual = bool.Parse(fileFound.Groups["Virtual"].Value);
            string code = fileFound.Groups["Content"].Value;

            yield return new(path, code, pureVirtual);
        }
    }

    [Obsolete("Must not be used without parameters", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public CodeFile() : this(default, default, default) { }
};

public static class CodeFileExtensions
{
    public static bool IsCharpFile(this CodeFile codeFile) 
        => codeFile.Path.FileName.ToLower().EndsWith(".cs", ".razor");
}