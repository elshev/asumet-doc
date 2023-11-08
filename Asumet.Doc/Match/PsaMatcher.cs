namespace Asumet.Doc.Match
{
    using Asumet.Doc.Office;
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatcher : MatcherBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="matchPattern">Match Pattern</param>
        /// <param name="officeExporter">Office Exporter</param>
        public PsaMatcher(IMatchPattern<Psa> matchPattern, IOfficeExporter<Psa> officeExporter)
            : base(matchPattern, officeExporter)
        {
        }
    }
}
