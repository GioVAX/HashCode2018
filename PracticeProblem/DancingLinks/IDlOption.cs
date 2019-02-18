using System.Collections.Generic;

namespace DancingLinks
{
    public interface IDlOption<out T>
    {
        IEnumerable<T> Items { get; }
    }
}