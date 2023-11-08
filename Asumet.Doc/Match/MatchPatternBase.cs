namespace Asumet.Doc.Match
{
    using System.Collections.Generic;
    using System.IO;
    using Asumet.Doc.Common;

    /// <summary>
    /// A Document match pattern that is stored in file
    /// </summary>
    /// <typeparam name="T">Type of the exported object. Ex.: Psa</typeparam>
    public abstract class MatchPatternBase<T> : IMatchPattern<T>
        where T : class
    {
        /// <inheritdoc/>
        public virtual string PatternFileName
        {
            get
            {
                return Path.ChangeExtension(DocumentName, AppSettings.Instance.WordMatchPatternExtension);
            }
        }

        /// <summary> Gets the document name, e.g. "ПСА" </summary>
        protected abstract string DocumentName { get; }

        /// <inheritdoc/>
        public IEnumerable<string> GetPattern()
        {
            return File.ReadAllLines(GetPatternFilePath());
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetFilledPattern(T documentObject)
        {
            var patternLines = GetPattern();
            var result = FillPatternPlaceholders(patternLines, documentObject);

            return result;
        }

        /// <summary>
        /// Gets the full path to the match pattern file.
        /// </summary>
        /// <returns>The full path to the match pattern file.</returns>
        protected virtual string GetPatternFilePath()
        {
            return Path.Combine(AppSettings.Instance.MatchPatternsDirectory, PatternFileName);
        }

        /// <summary>
        /// Fills placeholders in <paramref name="patternLines"/> from <paramref="documentObject"/>
        /// </summary>
        /// <param name="patternLines">Text to fill</param>
        /// <returns> A new list of strings with replaced values./// </returns>
        protected static IEnumerable<string> FillPatternPlaceholders(IEnumerable<string> patternLines, T documentObject)
        {
            return DocHelper.ReplacePlaceholdersInStrings(patternLines, documentObject, false);
        }
    }
}
