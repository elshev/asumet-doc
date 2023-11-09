namespace Asumet.Doc.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public struct DistanceResult
    {
        public int Distance { get; internal set; }
        public double Score { get; internal set; }
    }

    /// <summary>
    /// Wraps Match lib.
    /// Contains useful methods to match
    /// </summary>
    public static class MatchHelper
    {
        /// <summary>Min Score that Matcher can return </summary>
        public const double MinScore = 0;

        /// <summary>Max Score that Matcher can return </summary>
        public const double MaxScore = 1;
        
        /// <summary>
        /// Finds the Levenshtein distanceResult between two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>Levenshtein distanceResult</returns>
        public static DistanceResult Distance(string? str1, string? str2, MatchOptions? matchOptions)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return new DistanceResult { Distance = int.MaxValue, Score = MinScore };
            }

            if (str1 == str2)
            {
                return new DistanceResult { Distance = 0, Score = MaxScore };
            }

            matchOptions ??= MatchOptions.DefaultOptions();

            const string blankStr = "";
            var s1 = str1;
            var s2 = str2;
            if (matchOptions.SymbolsToIgnore.Length > 0)
            {
                s1 = ReplaceChars(s1, matchOptions.SymbolsToIgnore, blankStr);
                s2 = ReplaceChars(s2, matchOptions.SymbolsToIgnore, blankStr);
            }

            if (matchOptions.IgnoreCase)
            {
                s1 = s1?.ToLower();
                s2 = s2?.ToLower();
            }

            var distance = Fastenshtein.Levenshtein.Distance(s1, s2);
            double score = MaxScore - ((double)distance / (double)Math.Max(s1?.Length ?? 0, s2?.Length ?? 0));
            
            return new DistanceResult { Distance = distance, Score = score };
        }

        /// <summary>
        /// Finds the Levenshtein distanceResult between two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <returns>Levenshtein distanceResult</returns>
        public static DistanceResult Distance(string? str1, string? str2)
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
                return MinScore;
            }

            var distanceResult = Distance(str1, str2, matchOptions);
            return distanceResult.Score;
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
        /// Note! See remarks!
        /// </summary>
        /// <param name="str">String to match</param>
        /// <param name="pattern">Pattern with unfilled placeholders</param>
        /// <param name="placeholderValues">Mapping "{Placeholder}" -> "Value"</param>
        /// <param name="matchOptions">Additional matchOptions when comparing</param>
        /// <returns>A match score: placeholderValue between 0 and 1 </returns>
        /// <remarks>This is an experimental method. Is not recommended to use now.</remarks>
        public static double MatchWithPattern(
            string str,
            string pattern,
            IDictionary<string, string> placeholderValues,
            MatchOptions? matchOptions = null)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(pattern))
            {
                return MinScore;
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
                    return MinScore;
                }

                var strValue = placeholderIndex + placeholderValue.Length > s.Length
                    ? s[placeholderIndex..]
                    : s.Substring(placeholderIndex, placeholderValue.Length);
                var distanceResult = Distance(placeholderValue, strValue, matchOptions);
                valuesDistance += distanceResult.Distance;
                valuesLength += Math.Max(placeholderValue.Length, strValue.Length);
                s = s[.. (placeholderIndex + 1)];
            }

            double result = valuesLength > 0
                ? MaxScore - ((double)valuesDistance / valuesLength)
                : MinScore;
            return result;
        }

        private static string? ReplaceChars(string? str, string[] stringsToReplace, string newStr)
        {
            if (str == null)
            {
                return str;
            }

            var sb = new StringBuilder(str);
            foreach (var s in stringsToReplace)
            {
                sb.Replace(s, newStr);
            }

            return sb.ToString();
        }
    }
}
