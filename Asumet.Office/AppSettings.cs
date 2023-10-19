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

        /// <summary>
        /// The directory where document templates are stored.
        /// </summary>
        public string TemplatesDirectory { get; set; } = "./templates";

        /// <summary>
        /// The output directory where documents will be exported.
        /// </summary>
        public string DocumentOutputDirectory { get; set; } = string.Empty;

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
