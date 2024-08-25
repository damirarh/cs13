using System.Text.RegularExpressions;
using FluentAssertions;

namespace cs13.PartialProperties;

public partial class WithCs13
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex IsNumeric { get; }

    [TestCase("123", true)]
    [TestCase("abc", false)]
    public void InvokeGeneratedPartialProperty(string input, bool expectedResult)
    {
        IsNumeric.IsMatch(input).Should().Be(expectedResult);
    }
}
