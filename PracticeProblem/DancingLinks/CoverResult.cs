using System.Collections.Generic;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        public CoverResult(IDlOption<TItem> option,
            Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> stack,
            IEnumerable<IDlOption<TItem>> otherOptions)
        {
            Option = option;
            Stack = stack;
            OtherOptions = otherOptions;
        }

        public IDlOption<TItem> Option { get; }
        public Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> Stack { get; }
        public IEnumerable<IDlOption<TItem>> OtherOptions { get; }
    }
}