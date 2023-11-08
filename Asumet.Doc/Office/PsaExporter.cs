namespace Asumet.Doc.Office
{
    using Asumet.Doc.Common;
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaExporter : WordExporterBase<Psa>
    {
        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
