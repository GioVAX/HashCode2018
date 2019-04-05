using System.Collections.Generic;

namespace DancingLinks.UnitTests
{
    public class TestOption<T> : IDlOption<T>
    {
        public TestOption(IEnumerable<T> items)
        {
            Items = items;
        }

        public IEnumerable<T> Items { get; }

        public int CompareTo(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
