using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        private readonly Stack<RemovedNodeWrapper<ItemHeader<TItem>>> _itemsRemoved;
        private readonly Stack<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>> _itemOptionsRemoved;

        public IEnumerable<TItem> Items => _itemsRemoved.Select(node => node.Value.Value.Item);

        public CoverResult()
        {
            _itemsRemoved = new Stack<RemovedNodeWrapper<ItemHeader<TItem>>>();
            _itemOptionsRemoved = new Stack<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>>();
        }

        public void AddItem(RemovedNodeWrapper<ItemHeader<TItem>> item) => _itemsRemoved.Push(item);

        public void AddOptionFromItem(RemovedNodeWrapper<IDlOption<TItem>> itemOption, TItem item) =>
            _itemOptionsRemoved.Push(Tuple.Create(item, itemOption));

        public void UncoverAll(LinkedList<ItemHeader<TItem>> items)
        {
            while (_itemsRemoved.TryPop(out var item))
            {
                if (item.PrevNode != null)
                    items.AddAfter(item.PrevNode, item.Value);
                else if (item.NextNode != null)
                    items.AddBefore(item.NextNode, item.Value);
                else
                    items.AddFirst(item.Value);
            }

            while (_itemOptionsRemoved.TryPop(out var removedNode))
            {
                var (item, node) = removedNode;

                var itemHeader = items.Find(new ItemHeader<TItem>(item));

                if (node.PrevNode != null)
                    itemHeader.Value.Options.AddAfter(node.PrevNode, node.Value);
                else if (node.NextNode != null)
                    itemHeader.Value.Options.AddBefore(node.NextNode, node.Value);
                else
                    itemHeader.Value.Options.AddFirst(node.Value);
            }
        }
    }
}
