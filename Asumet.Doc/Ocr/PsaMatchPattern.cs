namespace Asumet.Doc.Ocr
{
    using Asumet.Doc.Common;
    using Asumet.Models;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaMatchPattern : WordMatchPatternBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objectToExport">Object to export to a document</param>
        public PsaMatchPattern(Psa objectToExport)
            : base(objectToExport)
        {
        }

        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
