﻿namespace Asumet.Doc.Match
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
        /// <summary>Constructor </summary>
        public MatchPatternBase(IAppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        /// <inheritdoc/>
        public virtual string PatternFileName
        {
            get
            {
                return Path.ChangeExtension(DocumentName, AppSettings.WordMatchPatternExtension);
            }
        }

        /// <summary>Gets the document name, e.g. "ПСА"</summary>
        protected abstract string DocumentName { get; }
        
        /// <summary>Application settings</summary>
        public IAppSettings AppSettings { get; }

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
            return Path.Combine(AppSettings.MatchPatternsDirectory, PatternFileName);
        }

        /// <summary>
        /// Fills placeholders in <paramref name="patternLines"/> from <paramref name="documentObject"/>
        /// </summary>
        /// <param name="patternLines">Text to fill</param>
        /// <param name="documentObject">The object to take values from</param>
        /// <returns> A new list of strings with replaced values./// </returns>
        protected static IEnumerable<string> FillPatternPlaceholders(IEnumerable<string> patternLines, T documentObject)
        {
            return DocHelper.ReplacePlaceholdersInStrings(patternLines, documentObject, false);
        }
    }
}
