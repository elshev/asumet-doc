namespace Asumet.Doc.Match
{
    using Asumet.Doc.Ocr;
    using Asumet.Doc.Office;
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatcher : MatcherBase<Psa>
    {
        /// <inheritdoc/>
        public PsaMatcher(IMatchPattern<Psa> matchPattern, IOfficeExporter<Psa> officeExporter, IOcrWrapper ocrWrapper)
            : base(matchPattern, officeExporter, ocrWrapper)
        {
        }
    }
}
