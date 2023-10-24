using Asumet.Doc.Match;
namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Models;

    public class PsaMatcherTest : IntegrationTestBase
    {
        [Theory]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        [InlineData("PSA-01-300dpi.jpg")]
        [InlineData("PSA-01-300dpi.png")]
        [InlineData("PSA-01-144dpi.png")]
        public void TestMatchDocumentImageWithPattern_MatchesProperDocs(string imageFileName)
        {
            // Arrange
            var psa = GetPsa();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            var ocrFilePath = GetScanFilePath(imageFileName);
            
            // Act
            var score = matcher.MatchDocumentImageWithPattern(ocrFilePath);
            
            // Assert
            score.Should().BeGreaterThan(95);
        }
    }
}
