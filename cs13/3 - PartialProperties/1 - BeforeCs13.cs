using System.Text.RegularExpressions;
using FluentAssertions;

namespace cs13.PartialProperties;

public partial class BeforeCs13
{
    public partial class Partial
    {
        partial void PartialMissing();

        protected partial bool PartialTry(double input, out int output);

        public void PublicPartialMissing()
        {
            PartialMissing();
        }

        public bool PublicPartialTry(double input, out int output)
        {
            return PartialTry(input, out output);
        }
    }

    public partial class Partial
    {
        protected partial bool PartialTry(double input, out int output)
        {
            var intInput = (int)input;
            if (intInput == input)
            {
                output = intInput;
                return true;
            }
            else
            {
                output = 0;
                return false;
            }
        }
    }

    [Test]
    public void InvokeMissingPartialMethod()
    {
        var partial = new Partial();
        var action = () => partial.PublicPartialMissing();
        action.Should().NotThrow();
    }

    [TestCase(1.0, true, 1)]
    [TestCase(1.1, false, 0)]
    public void InvokeImplementedPartialMethod(
        double input,
        bool expectedResult,
        int expectedOutput
    )
    {
        var result = new Partial().PublicPartialTry(input, out var output);
        result.Should().Be(expectedResult);
        output.Should().Be(expectedOutput);
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex IsNumeric();

    [TestCase("123", true)]
    [TestCase("abc", false)]
    public void InvokeGeneratedPartialMethod(string input, bool expectedResult)
    {
        IsNumeric().IsMatch(input).Should().Be(expectedResult);
    }
}
