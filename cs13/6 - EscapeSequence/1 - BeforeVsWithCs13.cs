using FluentAssertions;

namespace cs13.EscapeSequence;
public class BeforeVsWithCs13
{
    [Test]
    public void EscapeSequence()
    {
        "\e".Should().Be("\u001b");
    }
}
