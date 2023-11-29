namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Entities;
    using Asumet.Doc.Match;
    using Asumet.Doc.Api;

    public class PsaMatcherTest : ApiIntegrationTestBase
    {
        public PsaMatcherTest(ApiTestWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        private IMatcher<Psa> GetPsaMatcher()
        {
            return GetService<IMatcher<Psa>>();
        }

        private async Task MatchPsa(
            string imageFileName,
            int psaId,
            MatchMode matchMode,
            int targetScore = 95)
        {
            // Arrange
            var psa = GetPsa(psaId);
            var matcher = GetPsaMatcher();
            matcher.Mode = matchMode;
            var scanFilePath = GetScanFilePath(imageFileName);

            // Act
            var score = await matcher.MatchDocumentImageWithPatternAsync(scanFilePath, psa);

            // Assert
            if (targetScore > 50)
            {
                score.Should().BeGreaterThanOrEqualTo(targetScore);
            }
            else
            {
                score.Should().BeLessThan(targetScore);
            }
        }
        
        [Theory]
        [InlineData("PSA-01-144dpi.png", 1)]
        [InlineData("PSA-01-300dpi.jpg", 1)]
        [InlineData("PSA-02-144dpi.jpg", 2)]
        [InlineData("PSA-02-300dpi.jpeg", 2)]
        public async Task TestMatchDocumentImageWithPattern_MatchesProperDocsInPatternMatchMode(
            string imageFileName,
            int psaId)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Pattern, 95);
        }

        [Theory]
        [InlineData("PSA-01-144dpi.png", 1)]
        [InlineData("PSA-01-300dpi.jpg", 1)]
        [InlineData("PSA-02-144dpi.jpg", 2)]
        [InlineData("PSA-02-300dpi.jpeg", 2)]
        public async Task TestMatchDocumentImageWithPattern_InDocumentMatchMode(
            string imageFileName,
            int psaId)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Document, 95);
        }

        [Theory]
        [InlineData("PSA-01-300dpi-left.jpeg")]
        [InlineData("PSA-01-300dpi-left05.jpg")]
        [InlineData("PSA-01-300dpi-right03.jpg")]
        public async Task TestMatchDocumentImageWithPattern_MatchesRotatedDocsInPatternMatchMode(string imageFileName, int psaId = 1)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Pattern, 70);
        }

        [Theory]
        [InlineData("PSA-01-300dpi-left.jpeg")]
        [InlineData("PSA-01-300dpi-left05.jpg")]
        [InlineData("PSA-01-300dpi-right03.jpg")]
        public async Task TestMatchDocumentImageWithPattern_MatchesRotatedDocsInDocumentMatchMode(string imageFileName, int psaId = 1)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Document, 70);
        }

        [Theory]
        [InlineData("PSA-Empty-144dpi.png")]
        //[InlineData("PSA-01-072dpi.jpg")]
        [InlineData("PSA-02-144dpi.jpg")]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        public async Task TestMatchDocumentImageWithPattern_DoesntMatchWrongDocsInDocumentMatchMode(string imageFileName, int psaId = 1)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Document, 50);
        }

        [Theory]
        [InlineData("PSA-Empty-144dpi.png")]
        //[InlineData("PSA-01-072dpi.jpg")]
        [InlineData("PSA-02-144dpi.jpg")]
        [InlineData("PSA-01-300dpi-left.jpg")]
        [InlineData("PSA-01-300dpi-right.jpg")]
        public async Task TestMatchDocumentImageWithPattern_DoesntMatchWrongDocsInPatternMatchMode(string imageFileName, int psaId = 1)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Pattern, 50);
        }

        [Theory]
        [InlineData("PSA-01-144dpi-RotateDown.png", 1)]
        [InlineData("PSA-01-144dpi-RotateLeft.png", 1)]
        [InlineData("PSA-02-300dpi-RotateRight.jpeg", 2)]
        public async Task TestMatchDocumentImageWithPattern_MatchRotatedImagesInDocumentMatchMode(
            string imageFileName,
            int psaId)
        {
            await MatchPsa(imageFileName, psaId, MatchMode.Document, 95);
        }
    }
}
