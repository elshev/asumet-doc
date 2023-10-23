namespace Asumet.Doc.Match
{
    using System;
    using System.Text;
    using Asumet.Doc.Common;

    /// <summary>
    /// Wraps Match lib.
    /// Contains useful methods to match
    /// </summary>
    public static class MatchHelper
    {
        /// <summary>
        /// Finds the Levenshtein distance between two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>Levenshtein distance</returns>
        public static int Distance(string? str1, string? str2, MatchOptions? matchOptions)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return int.MaxValue;
            }

            if (str1 == str2)
            {
                return 0;
            }

            matchOptions ??= MatchOptions.DefaultOptions;

            const char blankChar = ' ';
            var s1 = str1;
            var s2 = str2;
            if (matchOptions.SymbolsToIgnore.Length > 0)
            {
                s1 = ReplaceChars(s1, matchOptions.SymbolsToIgnore, blankChar);
                s2 = ReplaceChars(s2, matchOptions.SymbolsToIgnore, blankChar);
            }

            if (matchOptions.IgnoreCase)
            {
                s1 = s1?.ToLower();
                s2 = s2?.ToLower();
            }

            return Fastenshtein.Levenshtein.Distance(s1, s2);
        }

        /// <summary>
        /// Finds the Levenshtein distance between two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <returns>Levenshtein distance</returns>
        public static int Distance(string? str1, string? str2)
        {
            return Distance(str1, str2, MatchOptions.DefaultOptions);
        }

        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double Match(string? str1, string? str2, MatchOptions? matchOptions)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            int distance = Distance(str1, str2, matchOptions);
            double result = 1.0 - ((double)distance / (double)Math.Max(str1.Length, str2.Length));
            return result;
        }

        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double Match(string? str1, string? str2)
        {
            return Match(str1, str2, null);
        }

        /// <summary>
        /// Tries to match only the unfixed part of the text.
        /// </summary>
        /// <param name="str">String to match</param>
        /// <param name="filledPattern">Pattern with filled placeholders</param>
        /// <param name="pattern">Pattern with unfilled placeholders</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double MatchWithPattern(
            string str,
            string filledPattern,
            string pattern,
            MatchOptions? matchOptions = null)
        {
            if (str == filledPattern)
            {
                return 1;
            }

            var fixedPattern = DocHelper.RemovePlaceholders(pattern);
            var fixedLength = fixedPattern.Length;
            var fullLength = filledPattern.Length;
            var valuesLength = fullLength - fixedLength;
            var valuesPart = valuesLength / fullLength;
            var totalScore = Match(str, filledPattern, matchOptions);

            return 0;
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
