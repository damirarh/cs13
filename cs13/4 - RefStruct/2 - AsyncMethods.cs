using FluentAssertions;

namespace cs13.RefStruct;

public class AsyncMethods
{
    private static async Task<int> AsyncMethodWithRefStruct(string input, int delay)
    {
        var span = input.AsSpan();
        int hash = 0;
        int count = span.Length;
        for (int i = 0; i < count; i++)
        {
            // http://www.cse.yorku.ca/~oz/hash.html
            hash += (hash << 5) + span[i]; // (hash * 33) + c
            //await Task.Delay(delay);
        }
        await Task.Delay(delay);
        return hash;
    }

    [Test]
    public async Task AsyncMethodWithRefStruct()
    {
        var hash = await AsyncMethodWithRefStruct("NTK2024", 10);
        hash.Should().Be(1033454389);
    }

    private static async Task<int> AsyncMethodWithUnsafe(string input, int delay)
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
                    //await Task.Delay(delay);
                }
            }
        }
        await Task.Delay(delay);
        return hash;
    }

    [Test]
    public async Task AsyncMethodWithUnsafe()
    {
        var hash = await AsyncMethodWithUnsafe("NTK2024", 10);
        hash.Should().Be(1033454389);
    }
}
