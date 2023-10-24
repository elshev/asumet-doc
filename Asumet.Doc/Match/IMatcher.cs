namespace Asumet.Doc.Match
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for all Word marchers.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public interface IMatcher<T>
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
        /// OCRs a document from <paramref name="documentImageFilePath"/>
        /// then matches it with the pattern for <see cref="DocumentObject"/>
        /// </summary>
        /// <returns>Matching percentage (0-100)</returns>
        int MatchDocumentImageWithPattern(string documentImageFilePath);
    }
}
