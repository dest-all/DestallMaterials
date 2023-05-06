using DestallMaterials.CodeGeneration.Environment;
using DestallMaterials.WheelProtection.Extensions.Strings;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;

namespace DestallMaterials.CodeGeneration.Text
{
    public class FileWriter
    {
        private static readonly Regex _directoryPath = new Regex(@"(.*)\\(^\\)*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private readonly SolutionPathFinder _pathFinder;
        private readonly ILogger _logger;
        public static readonly Regex _outpuFilesParser = new Regex(@"<(\s)*File(\s)*Path(\s)*=(\s)*""(?<Path>.*?)"".*?>(?<Content>(.|\s)*?)<(\s)*/(\s)*File(\s)*>", RegexOptions.Compiled);

        private static readonly string[] ExceptionsForUnicode = { "proto" };

        static readonly IEnumerable<char> _emptySymbols = new char[] 
        {
            '\r', '\t', '\n', ' ', '/'
        };
        public static string RemoveTextTags(string str, int startingIndex = 0)
        {
            int tagOpenedAt = -1;
            bool textMet = false;
            for (int i = startingIndex; i < str.Length; i++)
            {
                char symbol = str[i];
                if (tagOpenedAt == -1 && symbol == '<')
                {
                    tagOpenedAt = i;
                }
                else if (tagOpenedAt != -1)
                {
                    if (symbol == '>')
                    {
                        var stringWithRemovedText = str[0..(tagOpenedAt)]
                            + str[(i + 1)..];
                        var result = RemoveTextTags(stringWithRemovedText, tagOpenedAt);
                        return result;
                    }
                    if (!_emptySymbols.Contains(symbol))
                    {
                        if (!textMet)
                        {
                            if (str[i..(i + 4)] != "text")
                            {
                                tagOpenedAt = -1;
                            }
                            else 
                            {
                                textMet = true;
                            }
                            i += 3;
                        }
                        else 
                        {
                            tagOpenedAt = -1;
                        }
                    }
                }
            }
            return str;
        }

        readonly ProjectCodeGenerationSettings _settings = new ProjectCodeGenerationSettings
        {
            
        };

        internal FileWriter(SolutionPathFinder pathFinder, ILogger logger)
        {
            _pathFinder = pathFinder;
            _logger = logger;


            Dictionary<string, string> lineCommentPatterns = new CodegenSettings().extensionLineCommentPatterns;
            SourceFileData.LoadLineCommentPatterns(lineCommentPatterns);
        }

        public static async Task WriteCreatingDirectoryAsync(FilePath filePath, string fileContent)
        {
            Directory.CreateDirectory(filePath.Directory);

            if (!ExceptionsForUnicode.Contains(filePath.Extension))
            {
                await File.WriteAllTextAsync(filePath.ToString(), fileContent, encoding: Encoding.Unicode);
            }
            else
            {
                await File.WriteAllTextAsync(filePath.ToString(), fileContent, Encoding.ASCII);
            }
        }

        public static async Task<bool> WriteIfDifferentAsync(FilePath filePath, string fileContent)
        {
            if (!File.Exists(filePath.ToString()))
            {
                await WriteCreatingDirectoryAsync(filePath, fileContent);
                return true;
            }

            var contentHash = fileContent.GetHashCode();
            var existingFileHash = (await File.ReadAllTextAsync(filePath.ToString())).GetHashCode();

            if (contentHash != existingFileHash)
            {
                await WriteCreatingDirectoryAsync(filePath, fileContent);
                return true;
            }

            return false;
        }



        public IReadOnlyList<SourceFileData> SplitIntoFiles(string content, string source = null)
        {
            var matches = _outpuFilesParser.Matches(content);

            var exceptions = new List<Exception>();
            var result = new List<SourceFileData>();

            foreach (var fileParsed in matches.ToList())
            {
                string path = fileParsed.Groups["Path"].Value;
                string code = fileParsed.Groups["Content"].Value;
                try
                {
                    path = _pathFinder.RelativeToAbsolutePath(path) ?? throw new Exception($"Invalid path provided: {path}");
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    continue;
                }

                result.Add(new SourceFileData
                {
                    Content = code,
                    FilePath = FilePath.Parse(path) ?? throw new InvalidOperationException($"Unable to parse {path} as file path."),
                    Source = source
                });
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
            return result;
        }

        public async Task<int> WriteSourceFilesAsync(IEnumerable<SourceFileData> splittedSourceFiles)
        {
            var writingException = new List<Exception>();
            int i = 0;

            foreach (var codeFile in splittedSourceFiles)
            {
                try
                {
                    codeFile.Normalize();
                    if (!codeFile.OverwriteProtected())
                    {
                        codeFile.FilePath.FileName += _settings.GeneratedFileSuffix;
                        codeFile.AddAutogeneratedComment();
                        await WriteIfDifferentAsync(codeFile.FilePath, codeFile.Content);
                        i++;
                    }
                    if (!File.Exists(codeFile.FilePath.ToString()))
                    {
                        throw new Exception($"Could not create file on path {codeFile.FilePath}");
                    }
                }
                catch (Exception ex)
                {
                    writingException.Add(new Exception($"File {codeFile.FilePath} - " + ex.Message));
                }
            }
            if (writingException.Any())
            {
                var agex = new AggregateException("Error(s) writing files", writingException);
                _logger.LogError($"{agex.Message}: {agex.InnerExceptions.Select(ex => ex.Message).Join("\n")}", agex);
            }
            return i;
        }
    }
}
