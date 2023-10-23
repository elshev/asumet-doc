using Asumet.Doc.Match;

namespace Asumet.Doc.Tests.Match
{
    public class MatchHelperTest
    {
        [Fact]
        public void TestMatch_WithDefaultMatchOptions()
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
            MatchHelper.Match("Value", "value").Should().Be(1);
            MatchHelper.Match("Value", "valuE").Should().Be(1);
            MatchHelper.Match("Va ue", "Value").Should().Be(0.8);
        }


        [Fact]
        public void TestMatch_DontIgnoreCase()
        {
            // Arrange
            var options = MatchOptions.DefaultOptions;
            options.IgnoreCase = false;

            // Act, Assert
            MatchHelper.Match("190876_@#Value", "190876_@#Value", options).Should().Be(1);
            MatchHelper.Match("Val", "Val1").Should().Be(0.75);
            MatchHelper.Match("Value", "value", options).Should().Be(0.8);
            MatchHelper.Match("Value", "valuE", options).Should().Be(0.6);
        }

        [Fact]
        public void TestMatch_WithIgnoreSymbolsOptions()
        {
            // Arrange
            var options = MatchOptions.IgnoreSymbolsOptions;

            // Act, Assert
            MatchHelper.Match("190876_@#Value", "190876_@#Value", options).Should().Be(1);
            MatchHelper.Match("a,b", "a.b", options).Should().Be(1);
            MatchHelper.Match("a;b", "a:b", options).Should().Be(1);
            MatchHelper.Match("A.b:C", "A b C", options).Should().Be(1);
            MatchHelper.Match("Some text with, comma and: semicolon", "Some text with. comma and; semicolon", options)
                .Should().Be(1);
            MatchHelper.Match("A.b:C", "AbC", options).Should().Be(0.6);
        }

        [Fact]
        public void TestMatchPattern()
        {
            // Arrange

            // Act, Assert
            MatchHelper.MatchWithPattern(
                string.Empty,
                "{Placeholder}",
                new Dictionary<string, string> { { "{Placeholder}", "Value" } })
                .Should().Be(0);

            MatchHelper.MatchWithPattern(
                "Value",
                "{Placeholder}",
                new Dictionary<string, string> { { "{Placeholder}", "Value" } })
                .Should().Be(1);

            MatchHelper.MatchWithPattern(
                "Fix: Value",
                "Fix: {Placeholder}",
                new Dictionary<string, string>
                {
                    { "{Placeholder}", "Value" }
                })
                .Should().Be(1);

            MatchHelper.MatchWithPattern(
                "Fixed long long long long long long long long text: Value",
                "Fixed long long long long long long long long text: {Placeholder}",
                new Dictionary<string, string>
                {
                    { "{Placeholder}", "Value" }
                })
                .Should().Be(1);

            MatchHelper.MatchWithPattern(
                "Fixed long long long long long long long long text: WrongText",
                "Fixed long long long long long long long long text: {Placeholder}",
                new Dictionary<string, string>
                {
                    { "{Placeholder}", "Value" }
                })
                .Should().Be(0);

            MatchHelper.MatchWithPattern(
                "Fix: v12345",
                "Fix: {Placeholder}",
                new Dictionary<string, string>
                {
                    { "{Placeholder}", "Value" }
                })
                .Should().BeGreaterThan(0.19).And.BeLessThan(0.21);

            MatchHelper.MatchWithPattern(
                "Fixed long long long long long long long long text: v12345",
                "Fixed long long long long long long long long text: {Placeholder}",
                new Dictionary<string, string>
                {
                    { "{Placeholder}", "Value" }
                })
                .Should().BeGreaterThan(0.19).And.BeLessThan(0.21);

            MatchHelper.MatchWithPattern(
                "One Val1 two val2 three",
                "One {p1} two {p2} three",
                new Dictionary<string, string>
                {
                    { "{p1}", "Val1" },
                    { "{p2}", "val2" }
                })
                .Should().Be(1);

            MatchHelper.MatchWithPattern(
                "SomeText Val1 Another val2 and then...",
                "One {p1} two {p2} three",
                new Dictionary<string, string>
                {
                    { "{p1}", "Val1" },
                    { "{p2}", "val2" }
                })
                .Should().BeLessThan(0.2);
        }
    }
}
