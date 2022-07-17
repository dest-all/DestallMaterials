using DestallMaterials.CodeGeneration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerationRail
{
    class FileWriter
    {
        private static readonly Regex _directoryPath = new Regex(@"(.*)\\(^\\)*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private readonly SolutionPathFinder _pathFinder;
        private static readonly Regex _outpuFilesParser = new Regex(@"<(\s)*File(\s)*Path(\s)*=(\s)*""(?<Path>.*)""(\s)*>(?<Content>(.|\s)*?)<(\s)*/(\s)*File(\s)*>", RegexOptions.Compiled);

        private static readonly string[] ExceptionsForUnicode = { "proto" };

        public static void WriteCreatingDirectory(FilePath filePath, string fileContent)
        {
            Directory.CreateDirectory(filePath.Directory);

            if (!ExceptionsForUnicode.Contains(filePath.Extension))
            {
                File.WriteAllText(filePath.ToString(), fileContent, encoding: Encoding.Unicode);
            }
            else
            {
                File.WriteAllText(filePath.ToString(), fileContent, Encoding.ASCII);
            }
        }

        public static bool WriteIfDifferent(FilePath filePath, string fileContent)
        {
            if (!File.Exists(filePath.ToString()))
            {
                WriteCreatingDirectory(filePath, fileContent);
                return true;
            }

            var contentHash = fileContent.GetHashCode();
            var existingFileHash = File.ReadAllText(filePath.ToString()).GetHashCode();

            if (contentHash != existingFileHash)
            {
                WriteCreatingDirectory(filePath, fileContent);
                return true;
            }

            return false;
        }

        public FileWriter(SolutionPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        internal IEnumerable<SourceFileData> SplitIntoFiles(string content, string source = null)
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
                    FilePath = FilePath.Parse(path),
                    Source = source
                });
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
            return result;
        }
    }
}
