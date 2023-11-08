namespace Asumet.Common
{
    /// <summary>
    /// Helper methods for <see cref="File"/>
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Compares two files byte to byte
        /// </summary>
        /// <param name="filePath1">File path for the first file</param>
        /// <param name="filePath2">File path for the first file</param>
        /// <returns>True if files are equal, otherwise - False</returns>
        public static bool AreFileContentsEqual(string filePath1, string filePath2)
        {
            return File.ReadAllBytes(filePath1).SequenceEqual(File.ReadAllBytes(filePath2));
        }
    }
}
