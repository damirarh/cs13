using FluentAssertions;

namespace cs13.IndexFromTheEnd;

public class WithCs13
{
    public class IndexersExample
    {
        private readonly char[] letters = new char[10];
        public char this[int i]
        {
            get { return letters[i]; }
            set { letters[i] = value; }
        }

        public int Length => letters.Length;
    }

    [Test]
    public void IndexerInInitializer()
    {
        var indexer = new IndexersExample
        {
            [1] = '1',
            [2] = '4',
            [3] = '9',
            [^1] = '5',
        };

        indexer[1].Should().Be('1');
        indexer[2].Should().Be('4');
        indexer[3].Should().Be('9');
        indexer[^1].Should().Be('5');
    }
}
