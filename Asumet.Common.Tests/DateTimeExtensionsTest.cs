namespace Asumet.Common.Tests
{
    public class DateTimeExtensionsTest
    {
        [Fact]
        public void TestSetKindUtcNullInput()
        {
            DateTime? input = null;
            DateTime? result = input.SetKindUtc();
            result.Should().BeNull();
        }

        [Fact]
        public void TestSetKindUtc_NonNullRegularDateInput()
        {
            DateTime? input = DateTime.Now;
            DateTime? result = input.SetKindUtc();
            result.Should().NotBeNull();
            Assert.NotNull(result);
            result.Value.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public void TestSetKindUtc_NonNullOffsetDateInput()
        {
            DateTime? input = DateTime.Now;
            DateTime withKindUtcInput = DateTime.SpecifyKind(input.Value, DateTimeKind.Utc);
            DateTime? result = withKindUtcInput.SetKindUtc();
            result.Should().NotBeNull();
            Assert.NotNull(result);
            result.Value.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public void TestSetKindUtc_UnspecifiedKindIsOverwritten()
        {
            DateTime? input = DateTime.Now;
            DateTime withKindUtcInput = DateTime.SpecifyKind(input.Value, DateTimeKind.Unspecified);
            DateTime? result = withKindUtcInput.SetKindUtc();
            result.Should().NotBeNull();
            Assert.NotNull(result);
            result.Value.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public void TestSetKindUtc_LocalKindIsOverwritten()
        {
            DateTime? input = DateTime.Now;
            DateTime withKindUtcInput = DateTime.SpecifyKind(input.Value, DateTimeKind.Local);
            DateTime? result = withKindUtcInput.SetKindUtc();
            result.Should().NotBeNull();
            Assert.NotNull(result);
            result.Value.Kind.Should().Be(DateTimeKind.Utc);
        }
    }
}
