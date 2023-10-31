namespace Asumet.Doc
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DocModule
    {
        public static bool IsInitialized { get; private set; } = false;

        public static void Initialize(IServiceCollection services, ConfigurationManager configuration)
        {
            if (IsInitialized)
            {
                return;
            }
/*
            services
                .AddScoped<IOfficeExporter<Psa>, PsaExporter>()
                .AddScoped<IMatchPattern<Psa>, PsaMatchPattern>()
                .AddScoped<IMatcher<Psa>, PsaMatcher>()
            ;
*/
            IsInitialized = true;
        }
    }
}