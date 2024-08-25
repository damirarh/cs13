using FluentAssertions;
using System.Collections;

namespace cs13.RefStruct;

public class InterfacesAndGenerics
{
    public ref struct ReadOnlySpanWrapper<T> : IReadOnlyList<T>
    {
        readonly ReadOnlySpan<T> _span;

        internal ReadOnlySpanWrapper(ReadOnlySpan<T> span)
        {
            _span = span;
        }

        public T this[int index] => _span[index];
        public int Count => _span.Length;

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    private static int GetCustomHash<T>(T list) where T : IReadOnlyList<char>, allows ref struct
    {
        int hash = 0;
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            // http://www.cse.yorku.ca/~oz/hash.html  
            hash += (hash << 5) + list[i]; // (hash * 33) + c
        }
        return hash;
    }

    [Test]
    public void CalculateHashForArray()
    {
        var array = "NTK2024".ToCharArray();
        var hash = GetCustomHash(array);
        hash.Should().Be(1033454389);
    }

    [Test]
    public void CalculateHashForSpan()
    {
        var span = "NTK2024".AsSpan();
        var wrapper = new ReadOnlySpanWrapper<char>(span);
        //var cast = (IReadOnlyList<char>)wrapper;
        var hash = GetCustomHash(wrapper);
        hash.Should().Be(1033454389);
    }
}
