using Asumet.Doc.Office;

namespace Asumet.Doc.Tests.Office
{
    public class OfficeHelperTest
    {
        [Fact]
        public void TestGetPlaceholders()
        {
            OfficeHelper.GetPlaceholders("Some text").Should().Equal(Array.Empty<string>());
            OfficeHelper.GetPlaceholders("Some text {value}")
                .Should().Equal(new string[] { "value" });
            OfficeHelper.GetPlaceholders("{value}")
                .Should().Equal(new string[] { "value" });
            OfficeHelper.GetPlaceholders("Some text {value} and {another value}")
                .Should().Equal(new string[] { "value", "another value" });
            OfficeHelper.GetPlaceholders($"Some text {{value}}{Environment.NewLine} and {{another value}}")
                .Should().Equal(new string[] { "value", "another value" });
            OfficeHelper.GetPlaceholders("Some text {value} and } {another value}")
                .Should().Equal(new string[] { "value", "another value" });
            OfficeHelper.GetPlaceholders("Some text {value} and { {another value}")
                .Should().Equal(new string[] { "value", " {another value" });
            OfficeHelper.GetPlaceholders("Some text {SomeProperty} and {ParentProperty.Child} and {ArrayProperty[].ArrayChild}")
                .Should().Equal(new string[] { "SomeProperty", "ParentProperty.Child", "ArrayProperty[].ArrayChild" });
        }
    }
}
