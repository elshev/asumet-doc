using Microsoft.Extensions.Configuration;

namespace Asumet.Doc
{
    public interface IAppSettings
    {
        /// <summary> AsumetDocDb Password </summary>
        string AsumetDocDbPassword { get; set; }

        /// <summary> The output directory where documents will be exported. /// </summary>
        string DocumentOutputDirectory { get; set; }

        /// <summary> The directory where match pattern files are stored. /// </summary>
        string MatchPatternsDirectory { get; set; }

        /// <summary> The directory where document templates are stored. /// </summary>
        string TemplatesDirectory { get; set; }

        /// <summary>
        /// Tesseract Trained Data Directory.
        /// Used by Tesseract library.
        /// </summary>
        string TesseractDataDirectory { get; set; }

        /// <summary> Word template file extension. /// </summary>
        string WordMatchPatternExtension { get; set; }

        /// <summary> Word template file extension. /// </summary>
        string WordTemplateExtension { get; set; }

        /// <summary>
        /// Updates configuration
        /// </summary>
        /// <param name="configuration">Cofiguration source</param>
        void UpdateConfiguration(IConfiguration configuration);
    }
}
