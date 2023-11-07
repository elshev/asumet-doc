namespace Asumet.Common
{
    /// <summary>
    /// Helper methods for <see cref="Path"/>
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets a temp folder for the current application
        /// </summary>
        /// <returns>Returns a temp folder</returns>
        public static string GetTempDirectory()
        {
            const string TempFolderName = "asumet";
            var result = Path.Combine(Path.GetTempPath(), TempFolderName);
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }

        /// <summary>
        /// Gets a temp file.
        /// </summary>
        /// <param name="extension">The extension of the new file</param>
        /// <returns>Returns a path to a temp file</returns>
        public static string GetTempFileName(string extension = ".tmp", bool createFile = true)
        {
            var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + extension;
            var result = Path.Combine(GetTempDirectory(), fileName);
            if (createFile)
            {
                File.Create(result).Dispose();
            }

            return result;
        }
    }
}
