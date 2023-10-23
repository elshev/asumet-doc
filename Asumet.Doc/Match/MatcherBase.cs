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
        public T DocumentObject
        {
            get { return MatchPattern.DocumentObject; }
        }

        private IMatchPattern<T> MatchPattern { get; }

        /// <inheritdoc/>
        public int MatchDocumentWithPattern(IEnumerable<string> documentLines)
        {
            if (documentLines == null || !documentLines.Any())
            {
                return 0;
            }

            var patternLines = MatchPattern.GetPattern()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            var patternFilledLines = MatchPattern.GetFilledPattern()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            if (!patternFilledLines.Any())
            {
                return 0;
            }

            double matchSum = 0;
            var matchOptions = MatchOptions.DefaultOptions;

            for (int i = 0; i < patternFilledLines.Count; i++)
            {
                foreach (var documentLine in documentLines)
                {
                    double score = MatchHelper.Match(documentLine, patternFilledLines[i], matchOptions);
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
    }
}
