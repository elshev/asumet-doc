namespace Asumet.Doc.Match
{
    using System;

    /// <summary>
    /// Wraps Match lib
    /// </summary>
    public static class MatchWrapper
    {
        /// <summary>
        /// Matches two strings
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <returns>A match score: value between 0 and 1 </returns>
        public static double Match(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            if (str1 == str2)
            {
                return 100;
            }

            int distance = Fastenshtein.Levenshtein.Distance(str1, str2);
            double result = 1.0 - ((double)distance / (double)Math.Max(str1.Length, str2.Length));
            return result;
        }
    }
}
