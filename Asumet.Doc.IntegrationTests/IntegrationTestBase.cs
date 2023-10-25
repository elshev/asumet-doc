using Asumet.Models;
using Microsoft.Extensions.Configuration;

namespace Asumet.Doc.IntegrationTests
{
    public abstract class IntegrationTestBase
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

    }
}
