namespace Asumet.Doc.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Asumet.Doc.Ocr;
    using Asumet.Doc.Office;

    /// <summary>
    /// A Document match pattern that is stored in file
    /// </summary>
    /// <typeparam name="T">Type of the exported object. Ex.: Psa</typeparam>
    public abstract class MatcherBase<T> : IMatcher<T>
        where T : class
    {
        /// <summary> Constructor. /// </summary>
        /// <param name="matchPattern">Match Pattern for this documentLines</param>
        public MatcherBase(
            IMatchPattern<T> matchPattern,
            IOfficeExporter<T> officeExporter
            )
        {
            MatchPattern = matchPattern;
            OfficeExporter = officeExporter;
        }

        /// <inheritdoc/>
        public MatchMode Mode { get; set; } = MatchMode.Document;
        
        private IMatchPattern<T> MatchPattern { get; }
        
        public IOfficeExporter<T> OfficeExporter { get; }

        /// <inheritdoc/>
        public int MatchDocumentWithPattern(IEnumerable<string> documentLines, T documentObject)
        {
            ArgumentNullException.ThrowIfNull(documentLines, nameof(documentLines));
            ArgumentNullException.ThrowIfNull(documentObject, nameof(documentObject));
            if (documentLines == null || !documentLines.Any())
            {
                return 0;
            }

            IList<string> patternLines = GetPattern(documentObject);
            var result = MatchHelper.MatchDocumentLinesWithPatternLines(documentLines, patternLines, Mode);
            return result;
        }


        /// <inheritdoc/>
        public int MatchDocumentImageWithPattern(string documentImageFilePath, T documentObject)
        {
            if (string.IsNullOrWhiteSpace(documentImageFilePath))
            {
                return 0;
            }
            ArgumentNullException.ThrowIfNull(documentObject, nameof(documentObject));

            var documentLines = DoOcr(documentImageFilePath);
            var result = MatchDocumentWithPattern(documentLines, documentObject);
            return result;
        }

        protected static IEnumerable<string> DoOcr(string imageFilePath)
        {
            var lines = OcrWrapper.ImageToStrings(imageFilePath);
            return lines;
        }

        /// <summary>
        /// For <paramref name="documentObject"/> returns pattern lines for it.
        /// The way of getting the result depends on the <see cref="Mode"/>
        /// See <see cref="MatchMode"/> documentation.
        /// </summary>
        /// <param name="documentObject"></param>
        /// <returns></returns>
        protected IList<string> GetPattern(T documentObject)
        {
            ArgumentNullException.ThrowIfNull(documentObject, nameof(documentObject));
            if (Mode == MatchMode.Pattern)
            {
                return GetPatternLines(documentObject);
            }

            return GetExportedLines(documentObject);
        }

        /// <summary>
        /// Loads a pattern from file first then fills its values from <paramref name="documentObject"/>
        /// </summary>
        /// <param name="documentObject">The object to get values from.</param>
        /// <returns>Filled pattern values</returns>
        private IList<string> GetPatternLines(T documentObject)
        {
            ArgumentNullException.ThrowIfNull(documentObject, nameof(documentObject));
            var patternLines = MatchPattern.GetPattern()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            
            var result = MatchPattern.GetFilledPattern(documentObject)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            return result;
        }
        
        /// <summary>
        /// Exports first <paramref name="documentObject"/> to Word file.
        /// Then converts from Word file to a plain text.
        /// </summary>
        /// <param name="documentObject">The object to get values from.</param>
        /// <returns>Plain text lines after conversion <paramref name="documentObject"/> -> Word -> Text</returns>
        private IList<string> GetExportedLines(T documentObject)
        {
            ArgumentNullException.ThrowIfNull(documentObject, nameof(documentObject));
            var wordFilePath = OfficeExporter.Export(documentObject);
            var options = new WordFileToTextOptions { SkipFirstTableRowCount = 1 };
            var result = WordWrapper.WordFileToText(wordFilePath, options)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            return result;
        }
    }
}
