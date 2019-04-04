using System;
using System.Collections.Generic;

namespace DancingLinks
{
    public interface IDlOption<out T> : IComparable
    {
        IEnumerable<T> Items { get; }
    }
}