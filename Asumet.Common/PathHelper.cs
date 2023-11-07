namespace Asumet.Common
{
    /// <summary>
    /// Helper methods for <see cref="Path"/>
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Creates directory for <paramref name="path"/> if it doesn't exist
        /// </summary>
        /// <param name="path">Path to file or directory</param>
        public static void CreatePathDirectoryIfNotExists(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directory) || Directory.Exists(directory))
            {
                return;
            }

            Directory.CreateDirectory(directory);
        }
        
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
            var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            fileName = Path.ChangeExtension(fileName, extension);
            var result = Path.Combine(GetTempDirectory(), fileName);
            if (createFile)
            {
                File.Create(result).Dispose();
            }

            return result;
        }
    }
}
