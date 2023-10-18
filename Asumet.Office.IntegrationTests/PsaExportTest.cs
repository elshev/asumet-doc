using Asumet.Models;
using Microsoft.Extensions.Configuration;

namespace Asumet.Office.IntegrationTests
{
    public class PsaExportTest
    {
        public PsaExportTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);
        }

        [Fact]
        public void TestExportPsa()
        {
            // Arrange
            var psa = Psa.GetPsaStub();
            var psaExporter = new PsaExporter(psa); 

            // Act
            psaExporter.Export();

            // Assert
            var filePath = psaExporter.OutputFilePath;
            filePath.Should().NotBeNull();
            File.Exists(filePath).Should().BeTrue();
            var fileName = Path.GetFileName(filePath);
            fileName.StartsWith("ПСА").Should().BeTrue();
            var fileExtension = Path.GetExtension(filePath);
            fileExtension.Should().Be(".docx");
        }
    }
}
