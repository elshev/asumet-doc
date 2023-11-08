namespace Asumet.Doc.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Asumet.Doc.Ocr;

    /// <summary>
    /// A Document match pattern that is stored in file
    /// </summary>
    /// <typeparam name="T">Type of the exported object. Ex.: Psa</typeparam>
    public abstract class MatcherBase<T> : IMatcher<T>
        where T : class
    {
        /// <summary> Constructor. /// </summary>
        /// <param name="matchPattern">Match Pattern for this documentLines</param>
        public MatcherBase(IMatchPattern<T> matchPattern)
        {
            MatchPattern = matchPattern;
        }

        /// <inheritdoc/>
        private IMatchPattern<T> MatchPattern { get; }

        /// <inheritdoc/>
        public int MatchDocumentWithPattern(IEnumerable<string> documentLines, T documentObject)
        {
            const double passRate = 0.7;
            if (documentLines == null || !documentLines.Any())
            {
                return 0;
            }

            var patternLines = MatchPattern.GetPattern()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            var patternFilledLines = MatchPattern.GetFilledPattern(documentObject)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            if (!patternFilledLines.Any())
            {
                return 0;
            }

            double matchSum = 0;
            var matchOptions = MatchOptions.IgnoreSymbolsOptions();

            for (int i = 0; i < patternFilledLines.Count; i++)
            {
                foreach (var documentLine in documentLines)
                {
                    double score = MatchHelper.Match(documentLine, patternFilledLines[i], matchOptions);
                    if (score > passRate)
                    {
                        matchSum += score;
                        break;
                    }
                }
            }

            var result = (int)Math.Round(matchSum / patternFilledLines.Count * 100);
            return result;
        }

        /// <inheritdoc/>
        public int MatchDocumentImageWithPattern(string documentImageFilePath, T documentObject)
        {
            if (string.IsNullOrWhiteSpace(documentImageFilePath))
            {
                return 0;
            }

            var documentLines = OcrWrapper.ImageToStrings(documentImageFilePath);
            var result = MatchDocumentWithPattern(documentLines, documentObject);
            return result;
        }
    }
}
