using FluentAssertions;

namespace cs13.OverloadResolution;

public class BeforeCs13
{
    public class DefaultPriority
    {
        public string Method(ReadOnlySpan<int> s) => "Span";

        public string Method(int[] a) => "Array";
    }

    [Test]
    public void ResolutionWithDefaultPriority()
    {
        int[] array = [1, 2, 3];
        var overload = new DefaultPriority();
        var choice = overload.Method(array);
        choice.Should().Be("Array");
    }
}
