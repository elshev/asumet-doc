namespace Asumet.Doc.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// A factory for bootstrapping an application in memory for WebApi tests.
    /// For example, replaces "appsettings.json"
    /// </summary>
    /// <remarks>https://github.com/dotnet/aspnetcore/issues/37680#issuecomment-1311716470</remarks>
    public class ApiTestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureHostConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json");
            });
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });

            builder.UseEnvironment("Development");
        }
    }
}
