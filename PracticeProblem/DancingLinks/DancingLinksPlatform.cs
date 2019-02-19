using System.Collections.Generic;

namespace DancingLinks
{
    public class DancingLinksPlatform<T>
    {
        private readonly DoublyLinkedList<IDlOption<T>> _options;
        private readonly DoublyLinkedList<T> _items;

        public IEnumerable<IDlOption<T>> Options => _options.Values;
        public IEnumerable<T> Items => _items.Values;

        public DancingLinksPlatform()
        {
            _options = new DoublyLinkedList<IDlOption<T>>();
            _items = new DoublyLinkedList<T>();
        }

        public void AddOption(IDlOption<T> option) => _options.AddValue(option);
    }
}