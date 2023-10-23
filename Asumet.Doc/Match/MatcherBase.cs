namespace Asumet.Doc.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Asumet.Doc.Ocr;

    /// <summary>
    /// A Document match pattern that is stored in file
    /// </summary>
    /// <typeparam name="T">Type of the exported object. Ex.: Psa</typeparam>
    public abstract class MatcherBase<T> : IMatcher<T>
        where T : class
    {
        /// <summary> Constructor. /// </summary>
        /// <param name="wordMatchPattern">Match Pattern for this documentLines</param>
        public MatcherBase(IMatchPattern<T> wordMatchPattern)
        {
            WordMatchPattern = wordMatchPattern;
        }

        /// <inheritdoc/>
        public T DocumentObject
        {
            get { return WordMatchPattern.DocumentObject; }
        }

        private IMatchPattern<T> WordMatchPattern { get; }

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
                s1 = ReplaceChars(s1, matchOptions.SymbolsToIgnore, blankChar);
                s2 = ReplaceChars(s2, matchOptions.SymbolsToIgnore, blankChar);
            }

            return MatchWrapper.Match(s1, s2);
        }

        /// <inheritdoc/>
        public int MatchDocumentWithPattern(IEnumerable<string> documentLines)
        {
            if (documentLines == null || !documentLines.Any())
            {
                return 0;
            }

            var patternLines = WordMatchPattern.GetPattern()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            var patternFilledLines = WordMatchPattern.GetFilledPattern()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            if (!patternFilledLines.Any())
            {
                return 0;
            }

            double matchSum = 0;

            for (int i = 0; i < patternFilledLines.Count; i++)
            {
                foreach (var documentLine in documentLines)
                {
                    double score = MatchWrapper.Match(documentLine, patternFilledLines[i]);
                    if (score > 0.7)
                    {
                        matchSum += score;
                        break;
                    }
                }
            }

            return (int)Math.Round(matchSum / patternFilledLines.Count * 100);
        }

        /// <inheritdoc/>
        public int MatchDocumentWithPattern(string documentImageFilePath)
        {
            if (string.IsNullOrWhiteSpace(documentImageFilePath))
            {
                return 0;
            }

            var documentLines = OcrWrapper.ImageToStrings(documentImageFilePath);
            return MatchDocumentWithPattern(documentLines);
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
