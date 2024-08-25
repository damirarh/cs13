using FluentAssertions;

namespace cs13.RefStruct;

public class Iterators
{
    private static IEnumerable<int> IteratorMethodWithRefStruct(string input, int delay)
    {
        var span = input.AsSpan();
        int hash = 0;
        int count = span.Length;
        for (int i = 0; i < count; i++)
        {
            // http://www.cse.yorku.ca/~oz/hash.html
            hash += (hash << 5) + span[i]; // (hash * 33) + c
            //yield return hash;
        }
        yield return hash;
    }

    [Test]
    public void IteratorMethodWithRefStruct()
    {
        var hash = IteratorMethodWithRefStruct("NTK2024", 10);
        hash.Should().Equal([1033454389]);
    }

    private static IEnumerable<int> IteratorMethodWithUnsafe(string input, int delay)
    {
        int hash = 0;
        unsafe
        {
            fixed (char* pointer = input)
            {
                int count = input.Length;
                char* current = &pointer[0];
                for (int i = 0; i < count; i++)
                {
                    // http://www.cse.yorku.ca/~oz/hash.html
                    hash += (hash << 5) + *current; // (hash * 33) + c
                    current++;
                    //yield return hash;
                }
            }
        }
        yield return hash;
    }

    [Test]
    public void IteratorMethodWithUnsafe()
    {
        var hash = IteratorMethodWithUnsafe("NTK2024", 10);
        hash.Should().Equal([1033454389]);
    }
}
