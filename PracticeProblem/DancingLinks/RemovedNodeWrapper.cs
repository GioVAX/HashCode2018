using System.Collections.Generic;

namespace DancingLinks
{
    public class RemovedNodeWrapper<TNodeValue>
    {
        public LinkedListNode<TNodeValue> Value { get; }
        public LinkedListNode<TNodeValue> PrevNode { get; }
        public LinkedListNode<TNodeValue> NextNode { get; }

        public RemovedNodeWrapper(LinkedListNode<TNodeValue> node)
        {
            Value = node;
            PrevNode = node.Previous;
            NextNode = node.Next;
        }
    }
}
