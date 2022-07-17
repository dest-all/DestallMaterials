using DestallMaterials.WheelProtection.Extensions.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.CodeGeneration.Extensions
{
    internal static class StringExtensions
    {
        public static uint CalculateSymbolsInFront(this string str, char symbol)
        {
            uint result = 0;
            var enumerator = str.GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current == symbol)
            {
                result++;
            }
            return result;
        }

        public static string ReplaceSeveralOccurencies(this string inputString, string replacedString, string replaceWith, uint number)
        {
            var splitted = inputString.Split(replacedString.ToCharArray());
            return splitted.Take((int)number).Join(replaceWith) + splitted.Skip((int)number).Join(replacedString);
        }

    }
}
