namespace Asumet.Doc.Ocr
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Asumet.Doc.Common;

    /// <summary>
    /// A Document match pattern that is stored in file
    /// </summary>
    /// <typeparam name="T">Type of the exported object. Ex.: Psa</typeparam>
    public abstract class DocMatchPattern<T> : IDocMatchPattern<T>
        where T : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objectToExport">Object to export to a document.</param>
        public DocMatchPattern(T objectToExport)
        {
            ObjectToExport = objectToExport;
        }

        /// <inheritdoc/>
        public T ObjectToExport { get; }

        /// <inheritdoc/>
        public virtual string PatternFileName
        {
            get
            {
                return Path.ChangeExtension(DocumentName, AppSettings.Instance.WordMatchPatternExtension);
            }
        }

        /// <summary>
        /// Gets the document name.
        /// </summary>
        protected abstract string DocumentName { get; }

        /// <summary>
        /// Loads the pattern file and fills its placeholders
        /// </summary>
        /// <returns>Lines of the pattern with filled placeholders</returns>
        public IEnumerable<string> GetFilledPattern()
        {
            var lines = File.ReadAllLines(GetPatternFilePath()).ToList();

            return lines;
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
        /// Fills placeholders in <paramref name="patternLines"/> from <see cref="ObjectToExport"/>
        /// </summary>
        /// <param name="patternLines">Text to fill</param>
        /// <returns> A new list of strings with replaced values./// </returns>
        protected IEnumerable<string> FillPatternPlaceholders(IEnumerable<string> patternLines)
        {
            return DocHelper.ReplacePlaceholdersInStrings(patternLines, ObjectToExport, false);
        }
    }
}
