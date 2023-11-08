﻿namespace Asumet.Doc.Services
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

    public static class ServicesModule
    {
        public static bool IsInitialized { get; private set; } = false;

        public static void Initialize(IServiceCollection services, ConfigurationManager configuration)
        {
            if (IsInitialized)
            {
                return;
            }

            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            RepositoryModule.Initialize(services, configuration);
            DocModule.Initialize(services, configuration);

            services
                .AddScoped<IPsaService, PsaService>()
                .AddScoped<IExportDocService, ExportDocService>()
                .AddScoped<IMatchService<Psa, int>, PsaMatchService>()
                .AddScoped<IPsaMatchService, PsaMatchService>()
            ;

            IsInitialized = true;
        }
    }
}