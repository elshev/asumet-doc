namespace Asumet.Doc.Match
{
    using Asumet.Doc.Ocr;
    using Asumet.Models;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatcher : WordMatcherBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordMatchPattern">Match Pattern</param>
        public PsaMatcher(IWordMatchPattern<Psa> wordMatchPattern)
            : base(wordMatchPattern)
        {
        }
    }
}
