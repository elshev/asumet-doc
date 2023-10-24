using Asumet.Doc.Match;
using Asumet.Models;

namespace Asumet.Doc.IntegrationTests.Match
{
    public class PsaMatchPatternTest : IntegrationTestBase
    {
        [Fact]
        public void TestGetFilledPattern_ReturnsFilledPlaceholders()
        {
            // Arrange
            var psa = Psa.GetPsaStub();
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
