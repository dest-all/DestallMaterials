using System.Text.RegularExpressions;

namespace DestallMaterials.CodeGeneration.Text;

public class SolutionPathFinder
{
    public const string GuidPattern = @".{8}-.{4}-.{4}-.{4}-.{12}";
    public const string SourceGenerationProjectBaseName = "SourceGeneration";
    private static readonly Regex sourceGenerationProjectRegex = new Regex($".*{SourceGenerationProjectBaseName}_{GuidPattern}.csproj", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private string _rootProjectPath;
    private string _rootProject;
    private Dictionary<string, string> _projectsLocations = new Dictionary<string, string>();

    public static SolutionPathFinder Create(string rootProjectPath)
    {
        SolutionPathFinder pathFinder;

        if (Directory.Exists(rootProjectPath))
        {
            rootProjectPath = Directory.GetFiles(rootProjectPath).FirstOrDefault(f => f.EndsWith(".sln") || f.EndsWith(".csproj") || f.EndsWith(".shproj"))
                ?? throw new Exception($"No csproj, sln or shproj file in specified folder.");
        }

        if (rootProjectPath.EndsWith(".csproj"))
        {
            pathFinder = new SolutionPathFinder()
            {
                _rootProject = rootProjectPath.Split('\\').Last().Split(".csproj".ToCharArray()).First(),
                _rootProjectPath = Directory.GetParent(rootProjectPath).FullName
            };
        }
        else if (rootProjectPath.EndsWith(".shproj"))
        {
            pathFinder = new SolutionPathFinder()
            {
                _rootProject = rootProjectPath.Split('\\').Last().Split(".shproj".ToCharArray()).First(),
                _rootProjectPath = Directory.GetParent(rootProjectPath).FullName
            };
        }
        else if (rootProjectPath.EndsWith(".sln"))
        {
            pathFinder = new SolutionPathFinder()
            {
                _rootProject = rootProjectPath.Split('\\').Last().Split(".sln".ToCharArray()).First(),
                _rootProjectPath = Directory.GetParent(rootProjectPath).FullName
            };
        }
        else
        {
            throw new Exception($"Invalid project/solution file path '{rootProjectPath}'. Must end with '.csproj', '.sln' or '.shproj'.");
        }

        return pathFinder;
    }

    SolutionPathFinder()
    {

    }
    public string? RelativeToAbsolutePath(string relativeLocation)
    {
        relativeLocation = relativeLocation.Replace('/', Path.PathSeparator);
        string targetProject = relativeLocation.Split('\\').First();
        if (targetProject == null) { return null; }

        string targetProjectFolder;

        if (targetProject == _rootProject)
        {
            targetProjectFolder = _rootProjectPath;
        }
        else if (_projectsLocations.ContainsKey(targetProject))
        {
            targetProjectFolder = _projectsLocations[targetProject];
        }
        else
        {
            string? targetProjectFile = SeekInParentDirectories(Directory.GetParent(_rootProjectPath).FullName, targetProject + ".csproj").FirstOrDefault()
                ?? SeekInParentDirectories(Directory.GetParent(_rootProjectPath).FullName, targetProject + ".shproj").FirstOrDefault();


            if (targetProjectFile == null || !File.Exists(targetProjectFile))
            {
                return null;
            }

            targetProjectFolder = Directory.GetParent(targetProjectFile)?.FullName;
            if (targetProjectFolder != null) { _projectsLocations.Add(targetProject, targetProjectFolder); }

        }

        string result = targetProjectFolder + "\\" + string.Join("\\", relativeLocation.Split('\\').Skip(1));

        return result;
    }

    static IEnumerable<string> SeekInParentDirectories(string basePath, string seekedFileName, byte maximumHeight = 3)
    {
        string curDir = Directory.Exists(basePath) ? basePath : Directory.GetParent(basePath).FullName;
        string seekedFileLower = seekedFileName.ToLower();
        for (byte i = 0; i <= maximumHeight; i++)
        {
            IEnumerable<string> files;
            try
            {
                files = Directory.GetFiles(curDir, "*", SearchOption.AllDirectories).Where(f => f.Split('\\').Last().ToLower() == seekedFileLower);
            }
            catch (UnauthorizedAccessException)
            {
                files = null;
            }
            if (files == null || !files.Any()) { curDir = Directory.GetParent(curDir)?.FullName; if (curDir == null) { return null; } }
            else { return files; }
        }

        return new string[0];
    }

    public static IEnumerable<string> SeekInParentDirectories(string basePath, Regex regex, byte maximumHeight = 3)
    {
        string curDir = Directory.Exists(basePath) ? basePath : Directory.GetParent(basePath).FullName;
        for (byte i = 0; i <= maximumHeight; i++)
        {
            IEnumerable<string> files;
            try
            {
                files = Directory.GetFiles(curDir, "*", SearchOption.AllDirectories).Where(f => regex.IsMatch(f));
            }
            catch (UnauthorizedAccessException)
            {
                files = null;
            }
            if (files == null || !files.Any()) { curDir = Directory.GetParent(curDir)?.FullName; if (curDir == null) { return null; } }
            else { return files; }
        }

        return new string[0];
    }
}