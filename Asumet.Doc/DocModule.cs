namespace Asumet.Doc
{
    using Asumet.Doc.Match;
    using Asumet.Doc.Office;
    using Asumet.Entities;
    using Microsoft.Extensions.DependencyInjection;

    public static class DocModule
    {
        private static bool IsInitialized { get; set; } = false;

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