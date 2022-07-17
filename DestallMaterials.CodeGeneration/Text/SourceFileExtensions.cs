using DestallMaterials.CodeGeneration.Extensions;
using DestallMaterials.WheelProtection.Extensions.String;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DestallMaterials.CodeGeneration.Text
{
    static class SourceFileExtensions
    {
        private readonly static IEnumerable<char> PathForbiddenSymbols = Path.GetInvalidPathChars();
        private readonly static IEnumerable<char> NameForbiddenSymbols = Path.GetInvalidFileNameChars();
        internal static void Normalize(this SourceFileData sourceFile)
        {
            sourceFile.Content = FormatCode(sourceFile.FilePath, sourceFile.Content).Replace("<text>", "").Replace("</text>", "");
            sourceFile.Content = MinimizeFrontLines(sourceFile.Content).Trim();
        }

        public static string MinimizeFrontLines(string code)
        {
            string str = code.Replace("\t", "    ");
            var lines = str.Split("\r\n".ToCharArray()).Where(str => !string.IsNullOrEmpty(str));
            var minSpaces = lines.Min(s => s.CalculateSymbolsInFront(' '));
            var res = lines.Select(l => l.ReplaceSeveralOccurencies(" ", "", minSpaces)).Join("\n");
            return res;
        }

        private static string FormatCode(FilePath filePath, string content)
        {
            if (filePath.Extension == "cs")
            {
                return FormatWithRoslyn(content);
            }
            return content;
        }
        private static string FormatWithRoslyn(string csharpCode)
        {
            var tree = CSharpSyntaxTree.ParseText(csharpCode);
            var root = tree.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();
            return ret;
        }

        internal static void ValidateBeforeWriting(this SourceFileData file)
        {
            var errors = new List<Exception>();
            if (string.IsNullOrEmpty(file.Content?.Trim()))
            {
                errors.Add(new Exception("Content is empty"));
            }
            if (string.IsNullOrEmpty(file.FilePath.FileName))
            {
                errors.Add(new Exception("File name not provided."));
            }
            else
            {
                var nameForbiddenSymbols = NameForbiddenSymbols.Where(s => file.FilePath.FileName.Any(f => f == s));
                if (nameForbiddenSymbols.Any())
                {
                    errors.Add(new Exception($"File name contained forbidden chars: {nameForbiddenSymbols.Select(s => s.ToString()).Join(", ")}"));
                }
                if (string.IsNullOrEmpty(file.FilePath.Extension))
                {
                    errors.Add(new Exception("Extension not specified for the file."));
                }
            }
            var forbiddenCharsInDirectory = file.FilePath.Directory.Where(p => PathForbiddenSymbols.Any(s => s == p));
            if (forbiddenCharsInDirectory.Any())
            {
                errors.Add(new Exception($"Directory path contained forbidden symbols: {forbiddenCharsInDirectory.Select(s => s.ToString()).Join(", ")}"));
            }
            if (errors.Any())
            {
                throw new AggregateException(errors);
            }
        }
    }
}
