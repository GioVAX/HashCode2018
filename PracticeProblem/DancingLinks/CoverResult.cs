using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        private readonly Stack<RemovedNodeWrapper<IDlOption<TItem>>> _optionsRemoved;
        private readonly Stack<RemovedNodeWrapper<ItemHeader<TItem>>> _itemsRemoved;
        private readonly Stack<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>> _itemOptionsRemoved;

        public IEnumerable<IDlOption<TItem>> Options => _optionsRemoved.Select(node => node.Value.Value);
        public IEnumerable<TItem> Items => _itemsRemoved.Select(node => node.Value.Value.Item);

        public CoverResult()
        {
            _optionsRemoved = new Stack<RemovedNodeWrapper<IDlOption<TItem>>>();
            _itemsRemoved = new Stack<RemovedNodeWrapper<ItemHeader<TItem>>>();
            _itemOptionsRemoved = new Stack<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>>();
        }

        public void AddOption(RemovedNodeWrapper<IDlOption<TItem>> option) => _optionsRemoved.Push(option);

        public void AddItem(RemovedNodeWrapper<ItemHeader<TItem>> item) => _itemsRemoved.Push(item);

        public void AddOptionFromItem(RemovedNodeWrapper<IDlOption<TItem>> itemOption, TItem item) =>
            _itemOptionsRemoved.Push(Tuple.Create(item, itemOption));

        public void UncoverAll(LinkedList<IDlOption<TItem>> options, LinkedList<ItemHeader<TItem>> items)
        {
            while (_optionsRemoved.TryPop(out var option))
            {
                if (option.PrevNode != null)
                    options.AddAfter(option.PrevNode, option.Value);
                else if (option.NextNode != null)
                    options.AddBefore(option.NextNode, option.Value);
                else
                    options.AddFirst(option.Value);
            }

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
