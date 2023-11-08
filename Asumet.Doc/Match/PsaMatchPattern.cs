namespace Asumet.Doc.Match
{
    using Asumet.Doc.Common;
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatchPattern : MatchPatternBase<Psa>
    {
        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
