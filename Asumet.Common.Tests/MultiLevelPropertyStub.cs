namespace Asumet.Common.Tests
{
    internal class PropertyContainer
    {
        public string? Value { get; set; } = "Container PropertyValue";
    }

    internal class MultiLevelPropertyStub
    {

        public MultiLevelPropertyStub()
        {
            Level2 = new PropertyContainer();
        }

        public string Level1 { get; set; } = "Level1 PropertyValue";

        public PropertyContainer Level2 { get; set; }
    }
}
