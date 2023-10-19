using Asumet.Doc.Common;

namespace Asumet.Doc.Tests.Office
{
    public class OfficeHelperTest
    {
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
        }
    }
}
