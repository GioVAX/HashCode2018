using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        private readonly Stack<LinkedListNode<IDlOption<TItem>>> _optionsRemoved;
        private readonly Stack<LinkedListNode<ItemHeader<TItem>>> _itemsRemoved;

        public CoverResult()
        {
            _optionsRemoved = new Stack<LinkedListNode<IDlOption<TItem>>>();
            _itemsRemoved = new Stack<LinkedListNode<ItemHeader<TItem>>>();
        }

        public void AddOption(LinkedListNode<IDlOption<TItem>> option) => _optionsRemoved.Push(option);
        public void AddItem(LinkedListNode<ItemHeader<TItem>> item) => _itemsRemoved.Push(item);

        public IEnumerable<IDlOption<TItem>> Options => _optionsRemoved.Select(node => node.Value);
        public IEnumerable<TItem> Items => _itemsRemoved.Select(node=> node.Value.Item);
    }
}