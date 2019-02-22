using System.Collections.Generic;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        public CoverResult(IDlOption<TItem> option, Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> stack)
        {
            Option = option;
            Stack = stack;
        }

        public IDlOption<TItem> Option { get; }
        public Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> Stack { get; }
    }
}