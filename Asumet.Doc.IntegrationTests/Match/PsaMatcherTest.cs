using Asumet.Doc.Match;
namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Models;

    public class PsaMatcherTest : IntegrationTestBase
    {
        [Theory]
        [InlineData("PSA-01-300dpi.jpg")]
        [InlineData("PSA-01-300dpi.png")]
        [InlineData("PSA-01-144dpi.png")]
        [InlineData("PSA-01-300dpi-left05.jpg", 80)]
        [InlineData("PSA-01-300dpi-right03.jpg", 80)]
        public void TestMatchDocumentImageWithPattern_MatchesProperDocs(string imageFileName, int expectedScore = 95)
        {
            // Arrange
            var psa = GetPsa();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            var ocrFilePath = GetScanFilePath(imageFileName);
            
            // Act
            var score = matcher.MatchDocumentImageWithPattern(ocrFilePath);
            
            // Assert
            score.Should().BeGreaterThan(expectedScore);
        }
    }
}
