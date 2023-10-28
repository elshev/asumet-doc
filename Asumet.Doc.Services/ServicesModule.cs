namespace Asumet.Doc.Services
{
    using Asumet.Doc.Repo;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesModule
    {
        public static bool IsInitialized { get; private set; } = false;

        public static void Initialize(IServiceCollection services, ConfigurationManager configuration)
        {
            if (IsInitialized)
            {
                return;
            }

            RepositoryModule.Initialize(services, configuration);

            services
                .AddScoped<IPsaService, PsaService>()
            ;

            IsInitialized = true;
        }
    }
}