using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection.Tests.Extensions
{
    internal static class StringExtensions
    {
        internal static string FirstLine(this string text)
        {
            return text.Line(0);
        }

        internal static string Line(this string text, int lineNumber)
        {
            var splitted = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return splitted.ElementAt(lineNumber);
        }
    }
}
