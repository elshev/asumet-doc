using Asumet.Doc.Repo;
using Microsoft.EntityFrameworkCore;

namespace Asumet.Doc.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            var connectionString = builder.Configuration.GetConnectionString("AsumetDoc");
            connectionString = connectionString?.Replace("{password}", builder.Configuration["AsumetDocSecrets:AsumetDocDbPassword"]);
            builder.Services.AddDbContext<DocDbContext>(o => o.UseNpgsql(connectionString));
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}