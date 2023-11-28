namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Doc.Api;
    using Asumet.Doc.Match;
    using Asumet.Entities;

    public class PsaMatchPatternTest : ApiIntegrationTestBase
    {
        public PsaMatchPatternTest(ApiTestWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        private IMatchPattern<Psa> GetPsaMatcher()
        {
            return GetService<IMatchPattern<Psa>>();
        }
        
        [Fact]
        public void TestGetFilledPattern_ReturnsFilledPlaceholders()
        {
            // Arrange
            var psa = GetPsa();
            var matchPattern = GetPsaMatcher();

            // Act
            var result = matchPattern.GetFilledPattern(psa);

            // Assert
            var appSettings = GetService<IAppSettings>();
            var patternFilePath = Path.Combine(appSettings.MatchPatternsDirectory, matchPattern.PatternFileName);
            var patternLines = File.ReadAllLines(patternFilePath);
            result.Should().HaveCount(patternLines.Length);

            var lineIndex = 0;
            var lines = result.ToList();
            lines[lineIndex].Should().Contain(psa.ActNumber);
        }
    }
}
