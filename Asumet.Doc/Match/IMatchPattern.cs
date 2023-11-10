namespace Asumet.Doc.Match
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for all match patterns.
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
        /// Gets the pattern lines of strings
        /// </summary>
        /// <returns>Lines of the pattern</returns>
        IEnumerable<string> GetPattern();

        /// <summary>
        /// Loads the pattern file and fills its placeholders
        /// </summary>
        /// <param name="documentObject">The object to take values from</param>
        /// <returns>Lines of the pattern with filled placeholders</returns>
        IEnumerable<string> GetFilledPattern(T documentObject);
    }
}
