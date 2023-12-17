using System.Text.RegularExpressions;

namespace DestallMaterials.CodeGeneration.Text;

public class FilePath
{
    private static readonly Regex _path = new Regex(@"(.*)?\\(^\\)*(\..*)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public string Directory = "";
    public string FileName = "";
    public string Extension = "";

    public static FilePath? Parse(string filePath)
    {
        filePath = filePath.Replace('/', '\\');
        var res = _path.Match(filePath);
        if (!res.Success) { return null; }

        string nameWithExtension = filePath.Split('\\').Last();
        var s = nameWithExtension.Split('.');

        string fileName = string.Join(".", s.Take(Math.Max(s.Length - 1, 1)));

        string directory = res.Groups.Count > 0 ? res.Groups[0].Value : "";
        if (directory.EndsWith("\\"))
        {
            directory = string.Join("\\", directory.Split('\\').Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        var result = new FilePath
        {
            Directory = directory,
            FileName = fileName,
            Extension = s.Length > 1 ? s.Last() : ""
        };

        return result;
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Extension))
        {
            return $@"{Directory}\{FileName}.{Extension}";
        }
        else { return $@"{Directory}\{FileName}"; }
    }
}
