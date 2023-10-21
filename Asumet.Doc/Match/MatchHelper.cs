namespace Asumet.Doc.Match
{
    using System.Text;

    /// <summary>
    /// Addition options when matching
    /// </summary>
    public class MatchOptions
    {
        /// <summary> If symbols should be ignored when matching </summary>
        public bool IgnoreSymbols { get; set; } = true;

        /// <summary> Symbols to ignore when matching </summary>
        public char[] SymbolsToIgnore { get; set; } = new char[] { '.', ',', ':', ';', '/', '\\', '|' };
    }

    /// <summary>
    /// Contains methods to help do matching
    /// </summary>
    public static class MatchHelper
    {
        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <param name="matchOptions">Additional options when comparing</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double Match(string? str1, string? str2, MatchOptions? matchOptions = null)
        {
            const char blankChar = ' ';
            var s1 = str1;
            var s2 = str2;
            if (matchOptions != null)
            {
                s1 = s1.ReplaceChars(matchOptions.SymbolsToIgnore, blankChar);
                s2 = s2.ReplaceChars(matchOptions.SymbolsToIgnore, blankChar);
            }

            return MatchWrapper.Match(s1, s2);
        }

        private static string? ReplaceChars(this string? str, char[] charsToReplace, char newChar)
        {
            if (str == null)
            {
                return str;
            }

            var sb = new StringBuilder(str);
            foreach (var ch in charsToReplace)
            {
                sb.Replace(ch, newChar);
            }

            return sb.ToString();
        }
    }
}
