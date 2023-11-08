namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class RepositoryModule
    {
        public static bool IsInitialized { get; private set; } = false;

        public static void Initialize(IServiceCollection services, ConfigurationManager configuration)
        {
            if (IsInitialized)
            {
                return;
            }

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

            IsInitialized = true;
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