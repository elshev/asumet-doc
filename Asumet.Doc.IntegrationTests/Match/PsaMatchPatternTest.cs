namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Doc.Api;
    using Asumet.Doc.Match;
    using Asumet.Entities;
    using Microsoft.AspNetCore.Mvc.Testing;

    public class PsaMatchPatternTest : IntegrationTestBase
    {
        public PsaMatchPatternTest(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public void TestGetFilledPattern_ReturnsFilledPlaceholders()
        {
            // Arrange
            var psa = GetPsa();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern(psa);

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
