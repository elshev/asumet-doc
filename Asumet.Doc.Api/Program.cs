using Asumet.Doc.Repo;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Asumet.Doc.Api
{
    internal static class DocDbInitializerExtension
    {
        public static IApplicationBuilder SeedDocDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DocDbContext>();
                DocDbInitializer.Initialize(context);
            }
            catch (Exception)
            {

            }

            return app;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("AsumetDoc");
            connectionString = connectionString?.Replace("{password}", configuration["AsumetDocSecrets:AsumetDocDbPassword"]);
            builder.Services.AddDbContext<DocDbContext>(o => o.UseNpgsql(connectionString));
            builder.Services.AddScoped<DocDbInitializer>();

            builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.SeedDocDb();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}