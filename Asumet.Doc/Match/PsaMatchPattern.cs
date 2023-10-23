namespace Asumet.Doc.Match
{
    using Asumet.Doc.Common;
    using Asumet.Models;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatchPattern : MatchPatternBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="documentObject">Document object to export</param>
        public PsaMatchPattern(Psa documentObject)
            : base(documentObject)
        {
        }

        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
