namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Entities;
    using Asumet.Doc.Match;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Asumet.Doc.Api;

    public class PsaMatcherTest : IntegrationTestBase
    {
        public PsaMatcherTest(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        private static PsaMatcher GetPsaMatcher(int psaId = 1)
        {
            var psa = GetPsa(psaId);
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);
            var matcher = new PsaMatcher(matchPattern);
            return matcher;
        }

        [Theory]
        [InlineData("PSA-01-300dpi.jpg")]
        [InlineData("PSA-01-300dpi.png")]
        [InlineData("PSA-01-144dpi.png")]
        [InlineData("PSA-01-300dpi-left05.jpg", 80)]
        [InlineData("PSA-01-300dpi-right03.jpg", 80)]
        [InlineData("PSA-02-144dpi.png", 95, 2)]
        public void TestMatchDocumentImageWithPattern_MatchesProperDocs(
            string imageFileName,
            int minScore = 95,
            int psaId = 1)
        {
            // Arrange
            var matcher = GetPsaMatcher(psaId);
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = matcher.MatchDocumentImageWithPattern(scanFilePath);

            // Assert
            score.Should().BeGreaterThan(minScore);
        }

        [Theory]
        [InlineData("PSA-Empty-144dpi.png")]
        [InlineData("PSA-02-144dpi.png")]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        public void TestMatchDocumentImageWithPattern_DoesntMatchWrongDocs(string imageFileName, int maxScore = 33)
        {
            // Arrange
            var matcher = GetPsaMatcher();
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = matcher.MatchDocumentImageWithPattern(scanFilePath);

            // Assert
            score.Should().BeLessThan(maxScore);
        }
    }
}
