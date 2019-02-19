using System.Collections.Generic;

namespace DancingLinks
{
    public class DancingLinksPlatform<T>
    {
        private DoublyLinkedList<IDlOption<T>> _options;
        private DoublyLinkedList<T> _items;

        public IEnumerable<IDlOption<T>> Options => _options.Values;
        public IEnumerable<T> Items => _items.Values;

        public DancingLinksPlatform()
        {
            _options = new DoublyLinkedList<IDlOption<T>>();
            _items = new DoublyLinkedList<T>();
        }
    }
}