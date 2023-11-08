namespace Asumet.Doc.IntegrationTests
{
    using Asumet.Entities;
    using Microsoft.Extensions.Configuration;

    public abstract class IntegrationTestBase : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// Initializes AppSettings
        /// </summary>
        public IntegrationTestBase()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);
        }

        protected static Psa GetPsa(int id = 1)
        {
            return PsaSeedData.GetPsa(id);
        }

        protected static string GetScanFilePath(string fileName)
        {
            var result = Path.Combine("./TestInput/Scan", fileName);
            return result;            
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
