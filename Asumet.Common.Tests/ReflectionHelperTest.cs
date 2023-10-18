namespace Asumet.Common.Tests
{
    public class ReflectionHelperTest
    {
        [Fact]
        public void TestGetMemberValue()
        {
            MultiLevelPropertyStub mock = new MultiLevelPropertyStub();
            const string level1Value = "level1Value";
            mock.Level1 = level1Value;
            var value = ReflectionHelper.GetMemberValue(mock, "Level1");
            value.Should().NotBeNull()
                .And.Be(level1Value);
            const string level2Value = "level2Value";
            mock.Level2.Value = level2Value;
            value = ReflectionHelper.GetMemberValue(mock, "Level2.Value");
            value.Should().NotBeNull()
                .And.Be(level2Value);
        }
    }
}
