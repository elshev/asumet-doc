namespace Asumet.Doc
{
    using Asumet.Doc.Match;
    using Asumet.Doc.Ocr;
    using Asumet.Doc.Office;
    using Asumet.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>Module Initializer</summary>
    internal class DocModuleInitializer : ModuleBase
    {
        protected override void InternalInitialize(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IAppSettings, AppSettings>()
                .AddScoped<IOfficeExporter<Psa>, PsaExporter>()
                .AddScoped<IMatchPattern<Psa>, PsaMatchPattern>()
                .AddScoped<IMatcher<Psa>, PsaMatcher>()
                .AddScoped<IOcrWrapper, OcrWrapper>()
            ;
        }
    }

    /// <summary>Module</summary>
    public static class DocModule
    {
        private static DocModuleInitializer Initializer { get; } = new();
        
        /// <summary>
        /// Initializes this module
        /// </summary>
        /// <param name="services">Services to configure</param>
        /// <param name="configuration">Application configuration</param>
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            Initializer.Initialize(services, configuration);
        }
    }
}
