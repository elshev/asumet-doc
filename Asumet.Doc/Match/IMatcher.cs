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
        /// How to compare a recognized document
        /// See documentation in <see cref="MatchMode"/>
        /// </summary>
        MatchMode Mode { get; set; }
        
        /// <summary>
        /// Matches <paramref name="documentLines"/> with the pattern and returns matching percentage
        /// </summary>
        /// <param name="documentLines">The document text that should be matched with the pattern</param>
        /// <param name="documentObject">A documentLines to fill pattern from</param>
        /// <returns>Matching percentage (0-100)</returns>
        int MatchDocumentWithPattern(IEnumerable<string> documentLines, T documentObject);

        /// <summary>
        /// OCRs a documentLines from <paramref name="documentImageFilePath"/>
        /// then matches it with the pattern for <paramref name="documentObject"/>
        /// </summary>
        /// <param name="documentImageFilePath">The documents image to OCR and get text from</param>
        /// <param name="documentObject">A documentLines to fill pattern from</param>
        /// <returns>Matching percentage (0-100)</returns>
        int MatchDocumentImageWithPattern(string documentImageFilePath, T documentObject);
    }
}
