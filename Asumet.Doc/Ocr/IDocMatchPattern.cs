namespace Asumet.Doc.Ocr
{
    /// <summary>
    /// Interface for all march patterns.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public interface IDocMatchPattern<T>
        where T : class
    {
        /// <summary>
        /// Gets the object which is exported to a pattern file.
        /// </summary>
        public T ObjectToExport { get; }
    }
}
