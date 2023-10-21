using Asumet.Doc.Match;

namespace Asumet.Doc.Tests.Match
{
    public class MatchWrapperTest
    {
        [Fact]
        public void TestMatch()
        {
            MatchWrapper.Match(null, null).Should().Be(0);
            MatchWrapper.Match(null, "aaa").Should().Be(0);
            MatchWrapper.Match("aaa", null).Should().Be(0);
            MatchWrapper.Match(string.Empty, string.Empty).Should().Be(0);
            MatchWrapper.Match(string.Empty, "aaa").Should().Be(0);
            MatchWrapper.Match("aaa", string.Empty).Should().Be(0);

            MatchWrapper.Match("Value", "Value").Should().Be(1);
            MatchWrapper.Match("190876_@#Value", "190876_@#Value").Should().Be(1);

            MatchWrapper.Match("a", "a1").Should().Be(0.5);
            MatchWrapper.Match("Val", "Val1").Should().Be(0.75);
            MatchWrapper.Match("Value", "value").Should().Be(0.8);
            MatchWrapper.Match("Value", "valuE").Should().Be(0.6);
            MatchWrapper.Match("Va ue", "Value").Should().Be(0.8);

        }
    }
}
