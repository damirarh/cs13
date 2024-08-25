using FluentAssertions;

namespace cs13.ParamsCollections;

public class WithCs13Tests
{
    private string JoinNames(params string[] names) => "Array: " + string.Join(", ", names);

    private string JoinNames(params IEnumerable<string> names) =>
        "IEnumerable: " + string.Join(", ", names);

    private string JoinNames(params ReadOnlySpan<string?> names) =>
        "Span: " + string.Join(", ", names);

    [Test]
    public void ParamsCommaSeparated()
    {
        var joined = JoinNames("John", "Jane");
        joined.Should().Be("Span: John, Jane");
    }

    [Test]
    public void ParamsLinq()
    {
        var joined = JoinNames(Enumerable.Range(1, 2).Select(x => x.ToString()));
        joined.Should().Be("IEnumerable: 1, 2");
    }

    [Test]
    public void ParamsArray()
    {
        string[] names = ["John", "Jane"];
        var joined = JoinNames(names);
        joined.Should().Be("Array: John, Jane");
    }

    [Test]
    public void DotNet9SpanOverloads()
    {
        var joined = string.Join(", ", "John", "Jane");
        joined.Should().Be("John, Jane");
    }
}
