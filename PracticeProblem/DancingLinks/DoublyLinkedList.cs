using System.Collections.Generic;

namespace DancingLinks
{
    public class DoublyLinkedList<T>
    {
        public DoublyLinkedListNode<T> First { get; private set; }
        public DoublyLinkedListNode<T> Last { get; private set; }

        public IEnumerable<T> Values
        {
            get
            {
                if (First == null)
                    yield break;

                var node = First;
                do
                {
                    yield return node.Value;
                    node = node.Next;
                } while (node != First);
            }
        }

        public DoublyLinkedList() : this(new List<T>())
        { }

        public DoublyLinkedList(IEnumerable<T> values)
        {
            foreach (var value in values)
                AddValue(value);
        }

        public DoublyLinkedListNode<T> AddValue(T value)
        {
            var node = new DoublyLinkedListNode<T>(value);
            AppendNode(node);
            return node;
        }

        public void AppendNode(DoublyLinkedListNode<T> newNode)
        {
            if (First == null)
            {
                newNode.Previous = newNode.Next = newNode;
                First = Last = newNode;
            }
            else
            {
                newNode.Previous = Last;
                newNode.Next = First;

                First.Previous = Last.Next = newNode;
                Last = newNode;
            }
        }

        public void InsertNode(DoublyLinkedListNode<T> node)
        {
            if (node.Previous == null || node.Next == null)
                AppendNode(node);
            else
                node.Insert();
        }

        public DoublyLinkedListNode<T> RemoveNode(DoublyLinkedListNode<T> node)
        {
            if (node.Next == node)
                First = Last = null;
            else
                node.Remove();

            return node;
        }
    }

    public class DoublyLinkedListNode<TN>
    {
        public DoublyLinkedListNode(TN value) => Value = value;

        public DoublyLinkedListNode<TN> Previous { get; internal set; }
        public DoublyLinkedListNode<TN> Next { get; internal set; }

        public TN Value { get; }

        public void Remove()
        {
            Previous.Next = Next;
            Next.Previous = Previous;
        }

        public void Insert()
        {
            Previous.Next = this;
            Next.Previous = this;
        }
    }
}