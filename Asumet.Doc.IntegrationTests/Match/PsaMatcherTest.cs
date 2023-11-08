namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Entities;
    using Asumet.Doc.Match;
    using Asumet.Doc.Office;

    public class PsaMatcherTest : IntegrationTestBase
    {
        private static PsaMatcher GetPsaMatcher()
        {
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern();
            IOfficeExporter<Psa> exporter = new PsaExporter();
            var matcher = new PsaMatcher(matchPattern, exporter);
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
            var psa = GetPsa(psaId);
            var matcher = GetPsaMatcher();
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = matcher.MatchDocumentImageWithPattern(scanFilePath, psa);

            // Assert
            score.Should().BeGreaterThan(minScore);
        }

        [Theory]
        [InlineData("PSA-Empty-144dpi.png")]
        [InlineData("PSA-02-144dpi.png")]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        public void TestMatchDocumentImageWithPattern_DoesntMatchWrongDocs(string imageFileName, int maxScore = 35)
        {
            // Arrange
            var psa = GetPsa(1);
            var matcher = GetPsaMatcher();
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = matcher.MatchDocumentImageWithPattern(scanFilePath, psa);

            // Assert
            score.Should().BeLessThan(maxScore);
        }
    }
}
