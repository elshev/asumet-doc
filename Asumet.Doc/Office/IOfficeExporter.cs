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
        public T DocumentObject { get; }

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
        /// Gets the output document file path.
        /// </summary>
        /// <returns>Returns the output document file path.</returns>
        string OutputFilePath { get; }

        /// <summary>
        /// Exports an <see cref="DocumentObject"/> to a document file.
        /// </summary>
        void Export();
    }
}
