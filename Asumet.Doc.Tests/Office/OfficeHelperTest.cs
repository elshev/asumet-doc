using Asumet.Doc.Common;

namespace Asumet.Doc.Tests.Office
{
    public class OfficeHelperTest
    {
        [Fact]
        public void TestGetPlaceholders()
        {
            DocHelper.GetPlaceholders("Some text").Should().Equal(Array.Empty<string>());
            DocHelper.GetPlaceholders("Some text {value}")
                .Should().Equal(new string[] { "value" });
            DocHelper.GetPlaceholders("{value}")
                .Should().Equal(new string[] { "value" });
            DocHelper.GetPlaceholders("Some text {value} and {another value}")
                .Should().Equal(new string[] { "value", "another value" });
            DocHelper.GetPlaceholders($"Some text {{value}}{Environment.NewLine} and {{another value}}")
                .Should().Equal(new string[] { "value", "another value" });
            DocHelper.GetPlaceholders("Some text {value} and } {another value}")
                .Should().Equal(new string[] { "value", "another value" });
            DocHelper.GetPlaceholders("Some text {value} and { {another value}")
                .Should().Equal(new string[] { "value", " {another value" });
            DocHelper.GetPlaceholders("Some text {SomeProperty} and {ParentProperty.Child} and {ArrayProperty[].ArrayChild}")
                .Should().Equal(new string[] { "SomeProperty", "ParentProperty.Child", "ArrayProperty[].ArrayChild" });
        }
    }
}
