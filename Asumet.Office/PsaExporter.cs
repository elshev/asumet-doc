namespace Asumet.Office
{
    using Asumet.Models;

    /// <summary>
    /// Exports "ПСА" document to a Word file.
    /// </summary>
    public class PsaExporter : WordExporterBase<Psa>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objectToExport">Object to export to a document</param>
        public PsaExporter(Psa objectToExport)
            : base(objectToExport)
        {
        }

        /// <inheritdoc/>
        protected override string TemplateFileName => "ПСА.docx";
    }
}
