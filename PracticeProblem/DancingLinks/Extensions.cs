using System;
using System.Collections.Generic;
using System.Text;

namespace DancingLinks
{
    public static class Extensions
    {
        public static void ForEach<T>(this T item, Action<T> func) => func(item);
    }
}
