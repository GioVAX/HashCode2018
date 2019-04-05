using System;
using System.Collections.Generic;

namespace DancingLinks
{
    public class ItemHeader<TItem>
    {
        public readonly TItem Item;
        public readonly LinkedList<IDlOption<TItem>> Options;

        public ItemHeader(TItem item)
        {
            Item = item;
            Options = new LinkedList<IDlOption<TItem>>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ItemHeader<TItem> other))
                return false;

            return Item.Equals(other.Item);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item, Options);
        }
    }
}