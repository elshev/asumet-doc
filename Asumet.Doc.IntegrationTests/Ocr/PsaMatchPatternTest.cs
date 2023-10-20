using Asumet.Doc.Ocr;
using Asumet.Models;
using Microsoft.Extensions.Configuration;

namespace Asumet.Doc.IntegrationTests.Ocr
{
    public class PsaMatchPatternTest
    {
        public PsaMatchPatternTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings.Instance.UpdateConfiguration(configuration);
        }

        [Fact]
        public void TestGetFilledPattern_ReturnsFilledPlaceholders()
        {
            // Arrange
            var psa = Psa.GetPsaStub();
            var matchPattern = new PsaMatchPattern(psa);

            // Act
            var result = matchPattern.GetFilledPattern();

            // Assert
            var patternFilePath = Path.Combine(AppSettings.Instance.MatchPatternsDirectory, matchPattern.PatternFileName);
            var patternLines = File.ReadAllLines(patternFilePath);
            result.Should().HaveCount(patternLines.Length);

            var lineIndex = 0;
            var lines = result.ToList();
            lines[lineIndex].Should().Contain(psa.ActNumber);
        }
    }
}
