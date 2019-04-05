using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public partial class DancingLinksPlatform<TItem> where TItem : IComparable
    {
        private readonly LinkedList<IDlOption<TItem>> _options;
        private readonly LinkedList<ItemHeader<TItem>> _items;

        public IEnumerable<IDlOption<TItem>> Options => _options as IReadOnlyCollection<IDlOption<TItem>>;
        public IEnumerable<TItem> Items => _items.Select(hdr => hdr.Item);

        public DancingLinksPlatform()
        {
            _options = new LinkedList<IDlOption<TItem>>();
            _items = new LinkedList<ItemHeader<TItem>>();
        }

        public void AddOption(IDlOption<TItem> option)
        {
            _options.AddLast(option);

            foreach (var itemHeader in GetItemHeaders(option, true))
                itemHeader.Value.Options.AddLast(option);
        }

        private IEnumerable<LinkedListNode<ItemHeader<TItem>>> GetItemHeaders(IDlOption<TItem> option, bool createIfMissing = false) =>
            option.Items
                .Select(i => new ItemHeader<TItem>(i))
                .Select(ih => createIfMissing ? _items.Find(ih) ?? _AddItem(ih.Item) : _items.Find(ih));

        public CoverResult<TItem> Cover(IDlOption<TItem> option)
        {
            var ret = new CoverResult<TItem>();

            var itemHeaders = GetItemHeaders(option);

            foreach (var headerNode in itemHeaders)
            {
                _items.Remove(headerNode);
                ret.AddItem(headerNode);

                var optionsToRemove = headerNode.Value.Options
                    .Select(_options.Find)
                    .Where(opt => opt != null);

                foreach (var optionNode in optionsToRemove)
                {
                    _options.Remove(optionNode);
                    ret.AddOption(optionNode);
                }
            }

            return ret;
        }

        private LinkedListNode<ItemHeader<TItem>> _AddItem(TItem item) => _items.AddLast(new ItemHeader<TItem>(item));

        public void AddItem(TItem item) => _AddItem(item);
    }
}