namespace Asumet.Doc
{
    using Microsoft.Extensions.Configuration;

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
        public string DocumentOutputDirectory { get; set; } = string.Empty;

        /// <summary>
        /// Tesseract Trained Data Directory.
        /// Used by Tesseract library.
        /// </summary>
        public string TesseractDataDirectory { get; set; } = "./Tessdata";

        /// <summary>
        /// Updates configuration
        /// </summary>
        /// <param name="configuration">Cofiguration soutce</param>
        public void UpdateConfiguration(IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettingsSection.Bind(this);
        }
    }
}
