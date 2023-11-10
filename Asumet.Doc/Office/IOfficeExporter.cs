namespace Asumet.Doc.Office
{
    /// <summary>
    /// Interface for all document (Word, Excel) exporters.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public interface IOfficeExporter<T>
        where T : class
    {
        /// <summary>
        /// Gets a template file name with extension.
        /// </summary>
        string TemplateFileName { get; }

        /// <summary>
        /// If true - leave a placeholderName in the output document
        /// If false - replace it with the empty string
        /// </summary>
        bool LeaveMissingPlaceholders { get; set; }

        /// <summary>
        /// Exports an <paramref name="documentObject"/> to a document file.
        /// </summary>
        /// <param name="documentObject">An object to export values from.</param>
        /// <returns>The output file path</returns>
        string Export(T documentObject);
    }
}
