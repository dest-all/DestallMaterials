using System.Text.RegularExpressions;

namespace DestallMaterials.CodeGeneration.Text;

/// <summary>
/// Serves to find absolute paths corresponding to project-based relative file paths.
/// </summary>
public sealed class PathsProcessor
{    
    private readonly string _rootProjectPath;
    private readonly string _rootProject;

    internal PathsProcessor(string rootProject, string rootProjectPath)
    {
        _rootProjectPath = rootProjectPath;
        _rootProject = rootProject;
    }
    
    private readonly Dictionary<string, string> _projectsLocations = new Dictionary<string, string>();

    /// <summary>
    /// Only way to create PathFinder is based on passing some root file to it.
    /// </summary>
    /// <param name="rootFilePath">Path to .sln, .csproj or .shproj file</param>
    /// <returns>Ready for work <see cref="PathsProcessor"/></returns>
    /// <exception cref="FileLoadException">File is not one of the required.</exception>
    public static PathsProcessor Create(string rootFilePath)
    {
        PathsProcessor pathFinder;

        if (Directory.Exists(rootFilePath))
        {
            rootFilePath = Directory.GetFiles(rootFilePath).FirstOrDefault(f => f.EndsWith(".sln") || f.EndsWith(".csproj") || f.EndsWith(".shproj"))
                ?? throw new Exception($"No csproj, sln or shproj file in specified folder.");
        }

        if (rootFilePath.EndsWith(".csproj"))
        {
            pathFinder = new PathsProcessor(rootFilePath.Split('\\').Last().Split(".csproj".ToCharArray()).First(), Directory.GetParent(rootFilePath).FullName);
        }
        else if (rootFilePath.EndsWith(".shproj"))
        {
            pathFinder = new PathsProcessor(rootFilePath.Split('\\').Last().Split(".shproj".ToCharArray()).First(), Directory.GetParent(rootFilePath).FullName);
        }
        else if (rootFilePath.EndsWith(".sln"))
        {
            pathFinder = new PathsProcessor(rootFilePath.Split('\\').Last().Split(".sln".ToCharArray()).First(), Directory.GetParent(rootFilePath).FullName);
        }
        else
        {
            throw new FileLoadException($"Invalid project/solution file path '{rootFilePath}'. Must end with '.csproj', '.sln' or '.shproj'.");
        }

        return pathFinder;
    }

    /// <summary>
    /// Compose file system path for project-based path.
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <returns></returns>
    public string? ToAbsolutePath(ProjectRelativeFilePath sourceFilePath)
    {
        var relativeLocation = sourceFilePath.ToString();
        relativeLocation = relativeLocation.Replace("/", "\\");
        string targetProject = sourceFilePath.ProjectName;
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

    public static IEnumerable<string> SeekInParentDirectories(string basePath, string seekedFileName, byte maximumHeight = 3)
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