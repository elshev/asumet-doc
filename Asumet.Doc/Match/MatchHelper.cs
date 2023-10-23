namespace Asumet.Doc.Match
{
    using System;
    using System.Text;

    /// <summary>
    /// Wraps Match lib.
    /// Contains useful methods to match
    /// </summary>
    public static class MatchHelper
    {
        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double Match(string? str1, string? str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            if (str1 == str2)
            {
                return 1;
            }

            int distance = Fastenshtein.Levenshtein.Distance(str1, str2);
            double result = 1.0 - ((double)distance / (double)Math.Max(str1.Length, str2.Length));
            return result;
        }

        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <param name="matchOptions">Additional options when comparing</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double Match(string? str1, string? str2, MatchOptions? matchOptions)
        {
            const char blankChar = ' ';
            var s1 = str1;
            var s2 = str2;
            if (matchOptions != null)
            {
                s1 = ReplaceChars(s1, matchOptions.SymbolsToIgnore, blankChar);
                s2 = ReplaceChars(s2, matchOptions.SymbolsToIgnore, blankChar);
            }

            return Match(s1, s2);
        }

        private static string? ReplaceChars(string? str, char[] charsToReplace, char newChar)
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
