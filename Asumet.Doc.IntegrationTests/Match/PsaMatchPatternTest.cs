namespace Asumet.Doc.IntegrationTests.Match
{
    using Asumet.Doc.Match;
    using Asumet.Entities;

    public class PsaMatchPatternTest : IntegrationTestBase
    {
        [Fact]
        public void TestGetFilledPattern_ReturnsFilledPlaceholders()
        {
            // Arrange
            var psa = GetPsa();
            IMatchPattern<Psa> matchPattern = new PsaMatchPattern();

            // Act
            var result = matchPattern.GetFilledPattern(psa);

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
