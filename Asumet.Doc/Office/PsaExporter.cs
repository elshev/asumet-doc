namespace Asumet.Doc.Office
{
    using Asumet.Doc;
    using Asumet.Doc.Common;
    using Asumet.Entities;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaExporter : WordExporterBase<Psa>
    {
        /// <inheritdoc/>
        public PsaExporter(IAppSettings appSettings)
            : base(appSettings)
        {
        }

        /// <inheritdoc/>
        protected override string DocumentName => DocHelper.PsaDocumentName;
    }
}
