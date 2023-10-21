namespace Asumet.Doc.Office
{
    using Asumet.Doc.Common;
    using Asumet.Models;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaExporter : WordExporterBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="documentObject">Object to export to a document</param>
        public PsaExporter(Psa documentObject)
            : base(documentObject)
        {
        }

        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
