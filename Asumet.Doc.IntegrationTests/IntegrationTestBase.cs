using Microsoft.Extensions.Configuration;

namespace Asumet.Doc.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        public IntegrationTestBase()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);
        }
    }
}
