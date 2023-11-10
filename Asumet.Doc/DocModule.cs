namespace Asumet.Doc
{
    using Asumet.Doc.Match;
    using Asumet.Doc.Office;
    using Asumet.Entities;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Module Initializer
    /// </summary>
    public static class DocModule
    {
        private static bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Initializes this module
        /// </summary>
        /// <param name="services"></param>
        public static void Initialize(IServiceCollection services)
        {
            if (IsInitialized)
            {
                return;
            }

            services
                .AddScoped<IOfficeExporter<Psa>, PsaExporter>()
                .AddScoped<IMatchPattern<Psa>, PsaMatchPattern>()
                .AddScoped<IMatcher<Psa>, PsaMatcher>()
            ;

            IsInitialized = true;
        }
    }
}