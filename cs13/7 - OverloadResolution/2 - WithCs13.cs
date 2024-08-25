using System.Runtime.CompilerServices;
using FluentAssertions;

namespace cs13.OverloadResolution;

public class WithCs13
{
    public class ModifiedPriority
    {
        [OverloadResolutionPriority(1)]
        public string Method(ReadOnlySpan<int> s) => "Span";

        public string Method(int[] a) => "Array";
    }

    [Test]
    public void ResolutionWithModifiedPriority()
    {
        int[] array = [1, 2, 3];
        var overload = new ModifiedPriority();
        var choice = overload.Method(array);
        choice.Should().Be("Span");
    }
}
