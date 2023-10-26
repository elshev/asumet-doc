namespace Asumet.Doc.Match
{
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatcher : MatcherBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordMatchPattern">Match Pattern</param>
        public PsaMatcher(IMatchPattern<Psa> wordMatchPattern)
            : base(wordMatchPattern)
        {
        }
    }
}
