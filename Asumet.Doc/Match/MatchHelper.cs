namespace Asumet.Doc.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

            matchOptions ??= MatchOptions.DefaultOptions();

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
            return Distance(str1, str2, MatchOptions.DefaultOptions());
        }

        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>A match score: placeholderValue between 0 and 1 </returns>
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
        /// <returns>A match score: placeholderValue between 0 and 1 </returns>
        public static double Match(string? str1, string? str2)
        {
            return Match(str1, str2, null);
        }

        /// <summary>
        /// Tries to match only the unfixed part of the text.
        /// </summary>
        /// <param name="str">String to match</param>
        /// <param name="pattern">Pattern with unfilled placeholders</param>
        /// <param name="placeholderValues">Mapping "{Placeholder}" -> "Value"</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>A match score: placeholderValue between 0 and 1 </returns>
        public static double MatchWithPattern(
            string str,
            string pattern,
            IDictionary<string, string> placeholderValues,
            MatchOptions? matchOptions = null)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(pattern))
            {
                return 0;
            }

            string s = str;
            int valuesDistance = 0;
            int valuesLength = 0;
            foreach (var kvp in placeholderValues.Reverse())
            {
                var placeholder = kvp.Key;
                var placeholderValue = kvp.Value;
                var placeholderIndex = pattern.LastIndexOf(placeholder);
                if (placeholderIndex < 0 || placeholderIndex >= s.Length)
                {
                    return 0;
                }

                var strValue = placeholderIndex + placeholderValue.Length > s.Length
                    ? s[placeholderIndex..]
                    : s.Substring(placeholderIndex, placeholderValue.Length);
                var distance = Distance(placeholderValue, strValue, matchOptions);
                valuesDistance += distance;
                valuesLength += Math.Max(placeholderValue.Length, strValue.Length);
                s = s[.. (placeholderIndex + 1)];
            }

            double result = valuesLength > 0
                ? 1.0 - ((double)valuesDistance / valuesLength)
                : 0;
            return result;
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
