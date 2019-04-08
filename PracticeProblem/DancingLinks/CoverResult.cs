using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        private readonly Stack<RemovedNodeWrapper<IDlOption<TItem>>> _optionsRemoved;
        private readonly Stack<RemovedNodeWrapper<ItemHeader<TItem>>> _itemsRemoved;

        public IEnumerable<IDlOption<TItem>> Options => _optionsRemoved.Select(node => node.Value.Value);
        public IEnumerable<TItem> Items => _itemsRemoved.Select(node => node.Value.Value.Item);

        public CoverResult()
        {
            _optionsRemoved = new Stack<RemovedNodeWrapper<IDlOption<TItem>>>();
            _itemsRemoved = new Stack<RemovedNodeWrapper<ItemHeader<TItem>>>();
        }

        public void AddOption(RemovedNodeWrapper<IDlOption<TItem>> option) => _optionsRemoved.Push(option);

        public void AddItem(RemovedNodeWrapper<ItemHeader<TItem>> item) => _itemsRemoved.Push(item);

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
        }
    }
}