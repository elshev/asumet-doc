namespace Asumet.Doc.Match
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for all march patterns.
    /// </summary>
    /// <typeparam name="T">Type of the exported object.</typeparam>
    public interface IMatchPattern<T>
        where T : class
    {
        /// <summary>
        /// Gets a pattern file name with extension.
        /// </summary>
        string PatternFileName { get; }

        /// <summary>
        /// Gets the object which is exported to a pattern file.
        /// </summary>
        public T DocumentObject { get; }

        /// <summary>
        /// Gets the pattern lines of strings
        /// </summary>
        /// <returns>Lines of the pattern</returns>
        IEnumerable<string> GetPattern();

        /// <summary>
        /// Loads the pattern file and fills its placeholders
        /// </summary>
        /// <returns>Lines of the pattern with filled placeholders</returns>
        IEnumerable<string> GetFilledPattern();
    }
}
