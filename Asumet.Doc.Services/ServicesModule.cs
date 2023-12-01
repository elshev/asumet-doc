namespace Asumet.Doc.Services
{
    using Asumet.Doc.Repo;
    using Asumet.Doc.Services.Data;
    using Asumet.Doc.Services.Mapping;
    using Asumet.Doc.Services.Match;
    using Asumet.Doc.Services.Office;
    using Asumet.Doc.Services.Psas;
    using Asumet.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    /// <summary>Module Initializer</summary>
    internal class ServicesModuleInitializer : ModuleBase
    {
        protected override void InternalInitialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            RepositoryModule.Initialize(services, configuration);
            DocModule.Initialize(services, configuration);

            services
                .AddScoped<IPsaService, PsaService>()
                .AddScoped<IExportDocService, ExportDocService>()
                .AddScoped<IMatchService<Psa, int>, PsaMatchService>()
                .AddScoped<IPsaMatchService, PsaMatchService>()
            ;
        }
    }

    /// <summary>Module</summary>
    public static class ServicesModule
    {
        private static ServicesModuleInitializer Initializer { get; } = new();
        
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