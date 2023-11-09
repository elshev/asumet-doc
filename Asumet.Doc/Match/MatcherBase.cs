﻿namespace Asumet.Doc.Match
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
            const double passRate = 0.7;
            if (documentLines == null || !documentLines.Any())
            {
                return 0;
            }

            IList<string> patternLines;
            if (Mode == MatchMode.Pattern)
            {
                patternLines = GetPatternLines(documentObject);
            }
            else
            {
                patternLines = GetExportedLines(documentObject);
            }

            if (patternLines == null || !patternLines.Any())
            {
                return 0;
            }

            double matchSum = 0;
            var matchOptions = MatchOptions.IgnoreSymbolsOptions();
            var lines = documentLines.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            var lastLineIndex = 0;
            for (int patternIndex = 0; patternIndex < patternLines.Count; patternIndex++)
            {
                var patternLine = patternLines[patternIndex];
                int lineIndex = lastLineIndex;
                while (lineIndex < lines.Length)
                {
                    var documentLine = lines[lineIndex];
                    double score = MatchHelper.Match(documentLine, patternLine, matchOptions);
                    
                    // If we match in the Document mode (Object -> Word -> Text),
                    // try to improve by concatenating recognized consecutive lines
                    // Let's do it for lines with length more than minLineLength
                    const int minLineLength = 50;
                    if (score < passRate 
                        && Mode == MatchMode.Document 
                        && patternLine.Length > documentLine.Length 
                        && documentLine.Length > minLineLength)
                    {
                        var concatenatedLine = documentLine;
                        while (lineIndex < lines.Length - 1)
                        {
                            concatenatedLine += lines[lineIndex + 1];
                            double curScore = MatchHelper.Match(concatenatedLine, patternLine, matchOptions);
                            if (curScore <= score)
                            {
                                break;
                            }

                            score = curScore;
                            lineIndex++;
                        }
                    }
                    if (score >= passRate)
                    {
                        matchSum += score;
                        lastLineIndex = lineIndex + 1;
                        break;
                    }
                    
                    lineIndex++;
                }
            }

            var result = (int)Math.Round(matchSum / patternLines.Count * 100);
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

            var documentLines = OcrWrapper.ImageToStrings(documentImageFilePath);
            var result = MatchDocumentWithPattern(documentLines, documentObject);
            return result;
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
