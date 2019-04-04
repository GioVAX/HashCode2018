using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DancingLinksPlatform<TItem> where TItem : IComparable
    {

        private class ItemHeader
        {
            public TItem Value;
            public LinkedList<IDlOption<TItem>> Options;

            public ItemHeader(TItem value)
            {
                Value = value;
                Options = new LinkedList<IDlOption<TItem>>();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is ItemHeader other))
                    return false;

                return Value.Equals(other.Value);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Value, Options);
            }
        }

        private LinkedList<IDlOption<TItem>> _options;
        private LinkedList<ItemHeader> _items;

        public IEnumerable<IDlOption<TItem>> Options => _options as IReadOnlyCollection<IDlOption<TItem>>;
        public IEnumerable<TItem> Items => _items.Select(hdr => hdr.Value);

        public DancingLinksPlatform()
        {
            _options = new LinkedList<IDlOption<TItem>>();
            _items = new LinkedList<ItemHeader>();
        }

        public void AddOption(IDlOption<TItem> option)
        {
            _options.AddLast(option);

            foreach (var item in option.Items.Select(i => new ItemHeader(i)))
            {
                var x = _items.Find(item);
                if (x == null)
                    x = _AddItem(item.Value);

                x.Value.Options.AddLast(option);
            }
        }

        private LinkedListNode<ItemHeader> _AddItem(TItem item) => _items.AddLast(new ItemHeader(item));

        public void AddItem(TItem item) => _AddItem(item);
    }
}