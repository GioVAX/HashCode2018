using System;
using System.Collections.Generic;

namespace DancingLinks
{
    public class ItemHeader<TItem>
    {
        public readonly TItem Value;
        public readonly LinkedList<IDlOption<TItem>> Options;

        public ItemHeader(TItem value)
        {
            Value = value;
            Options = new LinkedList<IDlOption<TItem>>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ItemHeader<TItem> other))
                return false;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Options);
        }
    }
}