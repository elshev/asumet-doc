namespace Asumet.Doc.Match
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for all Word marchers.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public interface IWordMatcher<T>
        where T : class
    {
        /// <summary>
        /// Gets the object which is exported to a pattern file.
        /// </summary>
        public T DocumentObject { get; }

        /// <summary>
        /// Matches <paramref name="document"/> with the pattern and returns matching percentage
        /// </summary>
        /// <returns>Matching percentage (0-100)</returns>
        int MatchDocumentWithPattern(IEnumerable<string> document);

        /// <summary>
        /// Matches a document from <paramref name="documentFilePath"/>
        /// with the pattern and returns matching percentage
        /// </summary>
        /// <returns>Matching percentage (0-100)</returns>
        int MatchDocumentWithPattern(string documentFilePath);
    }
}
