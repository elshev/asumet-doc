namespace Asumet.Doc.IntegrationTests
{
    using Asumet.Doc.Api;
    using Asumet.Doc.Repo;
    using Asumet.Entities;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        /// <summary>
        /// Constructor.
        /// Initializes AppSettings
        /// </summary>
        public IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            Factory = factory;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);
        }

        protected WebApplicationFactory<Program> Factory { get; }

        /// <summary> Add here Psa Ids to remove on cleanup </summary>
        protected IList<int> PsasToDelete { get; } = new List<int>();

        protected static Psa GetPsa(int id = 1)
        {
            return PsaSeedData.GetPsa(id);
        }

        protected static string GetScanFilePath(string fileName)
        {
            var result = Path.Combine("./TestInput/Scan", fileName);
            return result;            
        }

        public void Dispose()
        {
            using var scope = Factory.Services.CreateScope();
            var psaRepository = scope.ServiceProvider.GetRequiredService<IPsaRepository>();
            foreach (var id in PsasToDelete)
            {
                psaRepository.RemoveEntity(id);
            }
        }
    }
}
