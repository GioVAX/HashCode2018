using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        private class kkk<K>
        {
            public LinkedListNode<K> Value { get; }
            public LinkedListNode<K> PrevNode { get; }
            public LinkedListNode<K> NextNode { get; }

            public kkk(LinkedListNode<K> node)
            {
                Value = node;
                PrevNode = node.Previous;
                NextNode = node.Next;
            }
        }

        private readonly Stack<kkk<IDlOption<TItem>>> _optionsRemoved;
        private readonly Stack<kkk<ItemHeader<TItem>>> _itemsRemoved;

        public IEnumerable<IDlOption<TItem>> Options => _optionsRemoved.Select(node => node.Value.Value);
        public IEnumerable<TItem> Items => _itemsRemoved.Select(node => node.Value.Value.Item);

        public CoverResult()
        {
            _optionsRemoved = new Stack<kkk<IDlOption<TItem>>>();
            _itemsRemoved = new Stack<kkk<ItemHeader<TItem>>>();
        }

        public void CoverOption(LinkedListNode<IDlOption<TItem>> option)
        {
            _optionsRemoved.Push(new kkk<IDlOption<TItem>>(option));
            option.List.Remove(option);
        }

        public void CoverItem(LinkedListNode<ItemHeader<TItem>> item)
        {
            _itemsRemoved.Push(new kkk<ItemHeader<TItem>>(item));
            item.List.Remove(item);
        }

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