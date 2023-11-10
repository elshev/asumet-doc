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
        [InlineData("PSA-01-144dpi.png", 1)]
        [InlineData("PSA-01-300dpi.jpg", 1)]
        [InlineData("PSA-02-144dpi.jpg", 2)]
        [InlineData("PSA-02-300dpi.jpeg", 2)]
        public async Task TestMatchDocumentImageWithPattern_MatchesProperDocsInPatternMatchMode(
            string imageFileName,
            int psaId,
            int minScore = 95)
        {
            // Arrange
            var psa = GetPsa(psaId);
            var matcher = GetPsaMatcher();
            matcher.Mode = MatchMode.Pattern;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            score.Should().BeGreaterThanOrEqualTo(minScore);
        }

        [Theory]
        [InlineData("PSA-01-144dpi.png", 1)]
        [InlineData("PSA-01-300dpi.jpg", 1)]
        [InlineData("PSA-02-144dpi.jpg", 2)]
        [InlineData("PSA-02-300dpi.jpeg", 2)]
        public async Task TestMatchDocumentImageWithPattern_InDocumentMatchMode(
            string imageFileName,
            int psaId,
            int minScore = 95)
        {
            // Arrange
            var psa = GetPsa(psaId);
            var matcher = GetPsaMatcher();
            matcher.Mode = MatchMode.Document;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            score.Should().BeGreaterThan(minScore);
        }

        [Theory]
        [InlineData("PSA-01-300dpi-left.jpeg")]
        [InlineData("PSA-01-300dpi-left05.jpg")]
        [InlineData("PSA-01-300dpi-right03.jpg")]
        public async Task TestMatchDocumentImageWithPattern_MatchesRotatedDocsInPatternMatchMode(string imageFileName, int psaId = 1)
        {
            // Arrange
            const int minScore = 70;
            var psa = GetPsa(psaId);
            var matcher = GetPsaMatcher();
            matcher.Mode = MatchMode.Pattern;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            score.Should().BeGreaterThanOrEqualTo(minScore);
        }

        [Theory]
        [InlineData("PSA-01-300dpi-left.jpeg")]
        [InlineData("PSA-01-300dpi-left05.jpg")]
        [InlineData("PSA-01-300dpi-right03.jpg")]
        public async Task TestMatchDocumentImageWithPattern_MatchesRotatedDocsInDocumentMatchMode(string imageFileName, int psaId = 1)
        {
            // Arrange
            const int minScore = 70;
            var psa = GetPsa(psaId);
            var matcher = GetPsaMatcher();
            matcher.Mode = MatchMode.Document;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            score.Should().BeGreaterThanOrEqualTo(minScore);
        }

        [Theory]
        [InlineData("PSA-Empty-144dpi.png")]
        [InlineData("PSA-01-072dpi.jpg")]
        [InlineData("PSA-02-144dpi.jpg", 50)]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        public async Task TestMatchDocumentImageWithPattern_DoesntMatchWrongDocsInDocumentMatchMode(string imageFileName, int maxScore = 35)
        {
            // Arrange
            var psa = GetPsa(1);
            var matcher = GetPsaMatcher();
            matcher.Mode = MatchMode.Document;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            score.Should().BeLessThan(maxScore);
        }

        [Theory]
        [InlineData("PSA-Empty-144dpi.png")]
        [InlineData("PSA-01-072dpi.jpg")]
        [InlineData("PSA-02-144dpi.jpg", 50)]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        public async Task TestMatchDocumentImageWithPattern_DoesntMatchWrongDocsInPatternMatchMode(string imageFileName, int maxScore = 35)
        {
            // Arrange
            var psa = GetPsa(1);
            var matcher = GetPsaMatcher();
            matcher.Mode = MatchMode.Pattern;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            score.Should().BeLessThan(maxScore);
        }

    }
}
