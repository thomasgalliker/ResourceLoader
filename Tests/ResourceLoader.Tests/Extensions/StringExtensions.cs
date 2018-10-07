using System.Linq;

namespace System.Reflection.Tests.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        ///     Returns the first line of the given
        ///     <param name="text">text</param>
        ///     parameter.
        /// </summary>
        internal static string FirstLine(this string text)
        {
            return text.Line(0);
        }

        /// <summary>
        ///     Returns the line number
        ///     <param name="lineNumber">lineNumber</param>
        ///     of the given
        ///     <param name="text">text</param>
        ///     .
        /// </summary>
        internal static string Line(this string text, int lineNumber)
        {
            var splitted = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return splitted.ElementAt(lineNumber);
        }
    }
}