namespace Asumet.Doc.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Asumet.Doc.Common;
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

            if (!patternLines.Any())
            {
                return 0;
            }

            double matchSum = 0;
            var matchOptions = MatchOptions.DefaultOptions();

            for (int i = 0; i < patternLines.Count; i++)
            {
                var placeholderNames = DocHelper.GetPlaceholderNames(patternLines[i]);
                var values = DocHelper.GetPlaceholderValues(DocumentObject, placeholderNames);
                var placeHolderValues = values.ToDictionary(
                    kvp => DocHelper.MakePlaceholder(kvp.Key),
                    kvp => kvp.Value ?? string.Empty);
                foreach (var documentLine in documentLines)
                {
                    double score = MatchHelper.MatchWithPattern(
                        documentLine,
                        patternLines[i],
                        placeHolderValues,
                        matchOptions);
                    if (score > 0.7)
                    {
                        matchSum += score;
                        break;
                    }
                }
            }

            return (int)Math.Round(matchSum / patternLines.Count * 100);
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
