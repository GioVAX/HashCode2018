using System.Collections.Generic;

namespace DancingLinks
{
    public class DancingLinksPlatform<T>
    {
        public IEnumerable<IDlOption<T>> Options { get; }
        public IEnumerable<T> Items { get; }

        public DancingLinksPlatform()
        {
            Options = new List<IDlOption<T>>();
            Items = new List<T>();
        }
    }
}