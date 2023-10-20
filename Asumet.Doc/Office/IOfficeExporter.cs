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
        /// Gets the object which will be exported to a document file.
        /// </summary>
        public T ObjectToExport { get; }

        /// <summary>
        /// Gets a template file name with extension.
        /// </summary>
        string TemplateFileName { get; }

        /// <summary>
        /// Gets the output document file path.
        /// </summary>
        /// <returns>Returns the output document file path.</returns>
        string OutputFilePath { get; }

        /// <summary>
        /// Exports an <see cref="ObjectToExport"/> to a document file.
        /// </summary>
        void Export();
    }
}
