﻿using Asumet.Doc.Match;

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
            var options = MatchOptions.DefaultOptions;

            // Act, Assert
            MatchHelper.MatchWithPattern(string.Empty, "Value", "{Placeholder}")
                .Should().Be(0);

            MatchHelper.MatchWithPattern("Value", "Value", "{Placeholder}")
                .Should().Be(1);

            MatchHelper.MatchWithPattern(
                "Fixed long long long long long long long long text: Value",
                "Fixed long long long long long long long long text: Value",
                "Fixed long long long long long long long long text: {Placeholder}")
                .Should().Be(1);

            double expectedValue = MatchHelper.Match("WrongText", "Value");
            MatchHelper.MatchWithPattern(
                "Fixed long long long long long long long long text: WrongText",
                "Fixed long long long long long long long long text: Value",
                "Fixed long long long long long long long long text: {Placeholder}")
                .Should().Be(expectedValue);
        }


    }
}
