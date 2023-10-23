using Asumet.Doc.Match;

namespace Asumet.Doc.Tests.Match
{
    public class MatchHelperTest
    {
        [Fact]
        public void TestMatch()
        {
            MatchHelper.Match(null, null).Should().Be(0);
            MatchHelper.Match(null, "aaa").Should().Be(0);
            MatchHelper.Match("aaa", null).Should().Be(0);
            MatchHelper.Match(string.Empty, string.Empty).Should().Be(0);
            MatchHelper.Match(string.Empty, "aaa").Should().Be(0);
            MatchHelper.Match("aaa", string.Empty).Should().Be(0);

            MatchHelper.Match("Value", "Value").Should().Be(1);
            MatchHelper.Match("190876_@#Value", "190876_@#Value").Should().Be(1);

            MatchHelper.Match("a", "a1").Should().Be(0.5);
            MatchHelper.Match("Val", "Val1").Should().Be(0.75);
            MatchHelper.Match("Value", "value").Should().Be(0.8);
            MatchHelper.Match("Value", "valuE").Should().Be(0.6);
            MatchHelper.Match("Va ue", "Value").Should().Be(0.8);
        }

        [Fact]
        public void TestMatch_WithDefaultMatchOptions()
        {
            // Arrange
            var options = new MatchOptions();

            // Act, Assert
            MatchHelper.Match(null, null, options).Should().Be(0);
            MatchHelper.Match(null, "aaa", options).Should().Be(0);
            MatchHelper.Match(string.Empty, "aaa", options).Should().Be(0);
            MatchHelper.Match("190876_@#Value", "190876_@#Value", options).Should().Be(1);
            MatchHelper.Match("Value", "value", options).Should().Be(0.8);
            MatchHelper.Match("Value", "valuE", options).Should().Be(0.6);

            MatchHelper.Match("a,b", "a.b", options).Should().Be(1);
            MatchHelper.Match("a;b", "a:b", options).Should().Be(1);
            MatchHelper.Match("A.b:C", "A b C", options).Should().Be(1);
            MatchHelper.Match("Some text, with comma : and semicolon", "Some text. with comma ; and semicolon", options)
                .Should().Be(1);
            MatchHelper.Match("A.b:C", "AbC", options).Should().Be(0.6);
        }
    }
}
