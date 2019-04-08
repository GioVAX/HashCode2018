using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DancingLinksPlatform<TItem> where TItem : IComparable
    {
        private readonly LinkedList<IDlOption<TItem>> _options;
        private readonly LinkedList<ItemHeader<TItem>> _items;

        public IEnumerable<IDlOption<TItem>> Options => _options.AsEnumerable();
        public IEnumerable<TItem> Items => _items.Select(hdr => hdr.Item);
        public IEnumerable<ItemHeader<TItem>> ItemHeaders => _items.AsEnumerable();

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

        public void AddItem(TItem item) => _AddItem(item);

        public void Uncover(CoverResult<TItem> coverResult) => coverResult.UncoverAll(_options, _items);
        public CoverResult<TItem> Cover(IDlOption<TItem> option)
        {
            var result = new CoverResult<TItem>();

            foreach (var headerNode in GetItemHeaders(option))
            {
                _RemoveItem(result, headerNode);

                headerNode.Value.Options
                    .Select(_options.Find)
                    .Where(opt => opt != null)
                    .ToList()
                    .ForEach(node => _RemoveOption(result, node));
            }

            return result;
        }

        private IEnumerable<LinkedListNode<ItemHeader<TItem>>> GetItemHeaders(IDlOption<TItem> option, bool createIfMissing = false) =>
            option.Items
                .Select(i => new ItemHeader<TItem>(i))
                .Select(ih => createIfMissing ? _items.Find(ih) ?? _AddItem(ih.Item) : _items.Find(ih));

        private LinkedListNode<ItemHeader<TItem>> _AddItem(TItem item) => _items.AddLast(new ItemHeader<TItem>(item));

        private void _RemoveOption(CoverResult<TItem> result, LinkedListNode<IDlOption<TItem>> optionNode)
        {
            result.AddOption(new RemovedNodeWrapper<IDlOption<TItem>>(optionNode));

            _options.Remove(optionNode);
        }

        private void _RemoveItem(CoverResult<TItem> result, LinkedListNode<ItemHeader<TItem>> headerNode)
        {
            result.AddItem(new RemovedNodeWrapper<ItemHeader<TItem>>(headerNode));
            _items.Remove(headerNode);
        }
    }
}