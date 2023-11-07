namespace Asumet.Doc
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Reflection;

    /// <summary>
    /// Application Settings.
    /// </summary>
    public class AppSettings
    {
        private static readonly object LockObject = new ();

        private static AppSettings? instance;

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static AppSettings Instance
        {
            get
            {
                lock (LockObject)
                {
                    instance ??= new AppSettings();

                    return instance;
                }
            }
        }

        /// <summary> The directory where document templates are stored. /// </summary>
        public string TemplatesDirectory { get; set; } = "./Templates";

        /// <summary> Word template file extension. /// </summary>
        public string WordTemplateExtension { get; set; } = ".docx";

        /// <summary> The directory where match pattern files are stored. /// </summary>
        public string MatchPatternsDirectory { get; set; } = "./Templates";

        /// <summary> Word template file extension. /// </summary>
        public string WordMatchPatternExtension { get; set; } = ".docx.txt";

        /// <summary> The output directory where documents will be exported. /// </summary>
        public string DocumentOutputDirectory { get; set; } = "./output";

        /// <summary>
        /// Tesseract Trained Data Directory.
        /// Used by Tesseract library.
        /// </summary>
        public string TesseractDataDirectory { get; set; } = "./Tessdata";

        /// <summary> AsumetDocDb Password </summary>
        public string AsumetDocDbPassword { get; set; } = string.Empty;

        /// <summary>
        /// Updates configuration
        /// </summary>
        /// <param name="configuration">Cofiguration source</param>
        public void UpdateConfiguration(IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettingsSection.Bind(this);
            var secretsSection = configuration.GetSection("AsumetDocSecrets");
            secretsSection.Bind(this);
            TemplatesDirectory = GetDirectoryFullPath(TemplatesDirectory);
            MatchPatternsDirectory = GetDirectoryFullPath(MatchPatternsDirectory);
        }

        private static string GetDirectoryFullPath(string directory)
        {
            if (Path.IsPathRooted(directory))
            {
                return directory;
            }

            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? AppDomain.CurrentDomain.BaseDirectory;
            var result = Path.Combine(basePath, directory);
            return result;
        }
    }
}
