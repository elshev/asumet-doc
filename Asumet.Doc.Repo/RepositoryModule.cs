namespace Asumet.Doc.Repo
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class RepositoryModule
    {
        public static void Initialize(IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("AsumetDoc");
            connectionString = connectionString?.Replace("{password}", configuration["AsumetDocSecrets:AsumetDocDbPassword"]);
            services.AddDbContext<DocDbContext>(o => o.UseNpgsql(connectionString));

            services.AddScoped<IBuyerRepository, BuyerRepository>();
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