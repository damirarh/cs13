using FluentAssertions;

namespace cs13.ParamsCollections
{
    public class BeforeCs13Tests
    {
        private string JoinNames(params string[] names) => "Array: " + string.Join(", ", names);

        [Test]
        public void ParamsCommaSeparated()
        {
            var joined = JoinNames("John", "Jane");
            joined.Should().Be("Array: John, Jane");
        }

        [Test]
        public void ParamsArray()
        {
            string[] names = ["John", "Jane"];
            var joined = JoinNames(names);
            joined.Should().Be("Array: John, Jane");
        }

        private string JoinNames(ReadOnlySpan<string?> names) =>
            "Span: " + string.Join(", ", names);

        [Test]
        public void NonParamsSpan()
        {
            var joined = JoinNames(["John", "Jane"]);
            joined.Should().Be("Span: John, Jane");
        }
    }
}
