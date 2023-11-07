namespace Asumet.Doc.Tests.Common
{
    using Asumet.Doc.Common;
    using Asumet.Entities;

    public class DocHelperTest
    {
        protected static Psa GetPsa(int id = 1)
        {
            return PsaSeedData.GetPsa(id);
        }

        [Fact]
        public void TestGetPlaceholders()
        {
            DocHelper.GetPlaceholderNames("Some text").Should().Equal(Array.Empty<string>());
            DocHelper.GetPlaceholderNames("Some text {value}")
                .Should().Equal(new string[] { "value" });
            DocHelper.GetPlaceholderNames("{value}")
                .Should().Equal(new string[] { "value" });
            DocHelper.GetPlaceholderNames("Some text {value} and {another value}")
                .Should().Equal(new string[] { "value", "another value" });
            DocHelper.GetPlaceholderNames($"Some text {{value}}{Environment.NewLine} and {{another value}}")
                .Should().Equal(new string[] { "value", "another value" });
            DocHelper.GetPlaceholderNames("Some text {value} and } {another value}")
                .Should().Equal(new string[] { "value", "another value" });
            DocHelper.GetPlaceholderNames("Some text {value} and { {another value}")
                .Should().Equal(new string[] { "value", " {another value" });
            DocHelper.GetPlaceholderNames("Some text {SomeProperty} and {ParentProperty.Child} and {ArrayProperty[].ArrayChild}")
                .Should().Equal(new string[] { "SomeProperty", "ParentProperty.Child", "ArrayProperty[].ArrayChild" });
        }

        [Fact]
        public void TestMakePlaceholder()
        {
            DocHelper.MakePlaceholder("").Should().Be("{}");
            DocHelper.MakePlaceholder("value").Should().Be("{value}");
            DocHelper.MakePlaceholder("Some text").Should().Be("{Some text}");
            DocHelper.MakePlaceholder("Parent.Child").Should().Be("{Parent.Child}");
        }

        [Fact]
        public void TestReplacePlaceholdersInString()
        {
            //Arrange
            var psa = GetPsa();

            // Act, Assert
            DocHelper.ReplacePlaceholdersInString("", psa, false).Should().Be("");
            DocHelper.ReplacePlaceholdersInString("Some text", psa, false).Should().Be("Some text");
            DocHelper.ReplacePlaceholdersInString("{ActNumber}", psa, false)
                .Should().Be($"{psa.ActNumber}");
            DocHelper.ReplacePlaceholdersInString("Some text {ActNumber} another text", psa, false)
                .Should().Be($"Some text {psa.ActNumber} another text");
            DocHelper.ReplacePlaceholdersInString("Some text {ActNumber} bla-bla {Buyer.FullName} another text", psa, false)
                .Should().Be($"Some text {psa.ActNumber} bla-bla {psa.Buyer?.FullName} another text");
        }

        [Fact]
        public void TestReplacePlaceholdersInString_SkipPlaceholders()
        {
            //Arrange
            var psa = GetPsa();

            // Act, Assert
            DocHelper.ReplacePlaceholdersInString("{WrongPlaceholder}", psa, false)
                .Should().Be("");
            DocHelper.ReplacePlaceholdersInString("{WrongPlaceholder}", psa, true)
                .Should().Be("{WrongPlaceholder}");
            DocHelper.ReplacePlaceholdersInString("One {WrongPlaceholder} two", psa, false)
                .Should().Be("One  two");
            DocHelper.ReplacePlaceholdersInString("One {WrongPlaceholder} two", psa, true)
                .Should().Be("One {WrongPlaceholder} two");
            DocHelper.ReplacePlaceholdersInString("One {WrongPlaceholder} two {ActNumber}", psa, false)
                .Should().Be($"One  two {psa.ActNumber}");
            DocHelper.ReplacePlaceholdersInString("One {WrongPlaceholder} two {ActNumber}", psa, true)
                .Should().Be($"One {{WrongPlaceholder}} two {psa.ActNumber}");
        }


        [Fact]
        public void TestReplacePlaceholdersInList()
        {
            //Arrange
            var psa = GetPsa();

            // Act, Assert
            DocHelper.ReplacePlaceholdersInStrings(Array.Empty<string>(), psa, false)
                .Should().Equal(Array.Empty<string>());
            DocHelper.ReplacePlaceholdersInStrings(new string[] { "s1", "s2" }, psa, false)
                .Should().Equal(new string[] { "s1", "s2" });
            DocHelper.ReplacePlaceholdersInStrings(new string[] { "First {ActNumber} and {WrongValue}", "Second {Buyer.FullName}" }, psa, false)
                .Should().Equal(new string[] { $"First {psa.ActNumber} and ", $"Second {psa.Buyer?.FullName}" });
        }

        [Fact]
        public void TestReplacePlaceholdersInList_SkipPlaceholders()
        {
            //Arrange
            var psa = GetPsa();

            // Act, Assert
            DocHelper.ReplacePlaceholdersInStrings(new string[] { "First {ActNumber} and {WrongValue}", "Second {Buyer.FullName}" }, psa, false)
                .Should().Equal(new string[] { $"First {psa.ActNumber} and ", $"Second {psa.Buyer?.FullName}" });
            DocHelper.ReplacePlaceholdersInStrings(new string[] { "First {ActNumber} and {WrongValue}", "Second {Buyer.FullName}" }, psa, true)
                .Should().Equal(new string[] { $"First {psa.ActNumber} and {{WrongValue}}", $"Second {psa.Buyer?.FullName}" });
        }

        [Fact]
        public void TestReplacePlaceholdersInList_ReturnsDifferentObject()
        {
            //Arrange
            var psa = GetPsa();
            var list = new List<string>() { "s1", "s2" };

            // Act
            var result = DocHelper.ReplacePlaceholdersInStrings(list, psa, false);

            // Assert
            ReferenceEquals(list, result).Should().BeFalse();
        }

        [Fact]
        public void TestRemovePlaceholders()
        {
            DocHelper.RemovePlaceholders(string.Empty).Should().Be(string.Empty);
            DocHelper.RemovePlaceholders("Some text").Should().Be("Some text");
            DocHelper.RemovePlaceholders("Some text {value}").Should().Be("Some text ");
            DocHelper.RemovePlaceholders($"Some text {{value}}{Environment.NewLine} and {{another value}}")
                .Should().Be($"Some text {Environment.NewLine} and ");
            DocHelper.RemovePlaceholders("Some text {SomeProperty} and {ParentProperty.Child} and {ArrayProperty[].ArrayChild}")
                .Should().Be("Some text  and  and ");
        }
    }
}
