namespace Asumet.Doc
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Reflection;

    /// <summary>
    /// Application Settings.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        public AppSettings(IConfiguration configuration)
        {
            UpdateConfiguration(configuration);
        }

        /// <inheritdoc/>
        public string TemplatesDirectory { get; set; } = "./Templates";

        /// <inheritdoc/>
        public string WordTemplateExtension { get; set; } = ".docx";

        /// <inheritdoc/>
        public string MatchPatternsDirectory { get; set; } = "./Templates";

        /// <inheritdoc/>
        public string WordMatchPatternExtension { get; set; } = ".docx.txt";

        /// <inheritdoc/>
        public string DocumentOutputDirectory { get; set; } = "./output";

        /// <inheritdoc/>
        public string TesseractDataDirectory { get; set; } = "./tessdata";

        /// <inheritdoc/>
        public string AsumetDocDbPassword { get; set; } = string.Empty;

        /// <inheritdoc/>
        public void UpdateConfiguration(IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettingsSection.Bind(this);
            var secretsSection = configuration.GetSection("AsumetDocSecrets");
            secretsSection.Bind(this);
            TemplatesDirectory = GetDirectoryFullPath(TemplatesDirectory);
            MatchPatternsDirectory = GetDirectoryFullPath(MatchPatternsDirectory);
            TesseractDataDirectory = GetDirectoryFullPath(TesseractDataDirectory);
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
