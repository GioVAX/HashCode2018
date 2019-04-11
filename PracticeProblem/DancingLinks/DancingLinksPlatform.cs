using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DancingLinksPlatform<TItem> where TItem : IComparable
    {
        private readonly LinkedList<ItemHeader<TItem>> _items;

        public IEnumerable<TItem> Items => _items.Select(hdr => hdr.Item);
        public IEnumerable<ItemHeader<TItem>> ItemHeaders => _items.AsEnumerable();

        public DancingLinksPlatform()
        {
            _items = new LinkedList<ItemHeader<TItem>>();
        }

        public void AddOption(IDlOption<TItem> option)
        {
            foreach (var itemHeader in GetItemHeaders(option, true))
                itemHeader.Value.Options.AddLast(option);
        }

        public void AddItem(TItem item) => _AddItem(item);

        public CoverResult<TItem> Cover(IDlOption<TItem> option)
        {
            var result = new CoverResult<TItem>(option);

            var headers = GetItemHeaders(option);

            headers.ForEach(hdr => _RemoveItem(result, hdr));

            headers
                .SelectMany(hdr => hdr.Value.Options)
                .Distinct()
                .ToList()
                .ForEach(opt => _RemoveOption(result, opt));

            return result;
        }

        public void Uncover(CoverResult<TItem> coverResult)
        {
            foreach (var coveredItem in coverResult.CoveredItems)
            {
                if (coveredItem.PrevNode != null)
                    _items.AddAfter(coveredItem.PrevNode, coveredItem.Value);
                else if (coveredItem.NextNode != null)
                    _items.AddBefore(coveredItem.NextNode, coveredItem.Value);
                else
                    _items.AddFirst(coveredItem.Value);
            }

            foreach (var (item, node) in coverResult.CoveredOptions)
            {
                var itemHeader = _items.Find(new ItemHeader<TItem>(item));

                if (node.PrevNode != null)
                    itemHeader.Value.Options.AddAfter(node.PrevNode, node.Value);
                else if (node.NextNode != null)
                    itemHeader.Value.Options.AddBefore(node.NextNode, node.Value);
                else
                    itemHeader.Value.Options.AddFirst(node.Value);
            }
        }

        private List<LinkedListNode<ItemHeader<TItem>>> GetItemHeaders(IDlOption<TItem> option,
            bool createIfMissing = false) =>
            option.Items
                .Select(i => new ItemHeader<TItem>(i))
                .Select(ih => createIfMissing ? _items.Find(ih) ?? _AddItem(ih.Item) : _items.Find(ih))
                .ToList();

        private LinkedListNode<ItemHeader<TItem>> _AddItem(TItem item) => _items.AddLast(new ItemHeader<TItem>(item));

        private void _RemoveOption(CoverResult<TItem> result, IDlOption<TItem> option)
        {
            var optionNodes = GetItemHeaders(option)
                .Where(node => node != null)
                .Select(node => Tuple.Create(node.Value.Item, node.Value.Options.Find(option)));

            foreach (var (item, node) in optionNodes)
            {
                result.AddOptionNode(Tuple.Create(item, new RemovedNodeWrapper<IDlOption<TItem>>(node)));
                node.List.Remove(node);
            }
        }

        private void _RemoveItem(CoverResult<TItem> result, LinkedListNode<ItemHeader<TItem>> headerNode)
        {
            result.AddItem(new RemovedNodeWrapper<ItemHeader<TItem>>(headerNode));
            _items.Remove(headerNode);
        }
    }
}