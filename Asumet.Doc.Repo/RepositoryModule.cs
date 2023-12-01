namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>Module Initializer</summary>
    internal class RepositoryModuleInitializer : ModuleBase
    {
        protected override void InternalInitialize(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AsumetDoc");
            connectionString = connectionString?.Replace("{password}", configuration["AsumetDocSecrets:AsumetDocDbPassword"]);
            services.AddDbContext<DocDbContext>(o => o.UseNpgsql(connectionString));

            services
                .AddScoped<IRepositoryBase<Buyer, int>, BuyerRepository>()
                .AddScoped<IBuyerRepository, BuyerRepository>()
                .AddScoped<IRepositoryBase<Supplier, int>, SupplierRepository>()
                .AddScoped<ISupplierRepository, SupplierRepository>()
                .AddScoped<IRepositoryBase<PsaScrap, int>, PsaScrapRepository>()
                .AddScoped<IPsaScrapRepository, PsaScrapRepository>()
                .AddScoped<IRepositoryBase<Psa, int>, PsaRepository>()
                .AddScoped<IPsaRepository, PsaRepository>()
            ;
        }
    }

    /// <summary>Module</summary>
    public static class RepositoryModule
    {
        private static RepositoryModuleInitializer Initializer { get; } = new();
        
        /// <summary>
        /// Initializes this module
        /// </summary>
        /// <param name="services">Services to configure</param>
        /// <param name="configuration">Application configuration</param>
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            Initializer.Initialize(services, configuration);
        }

        public static void SeedDocDb(IServiceProvider services)
        {
            try
            {
                var context = services.GetRequiredService<DocDbContext>();
                DocDbInitializer.Initialize(context);
            }
            catch (Exception)
            {

            }
        }

    }
}