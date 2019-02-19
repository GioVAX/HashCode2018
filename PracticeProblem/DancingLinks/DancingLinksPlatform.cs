using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DancingLinksPlatform<TItem>
    {
        private readonly DoublyLinkedList<IDlOption<TItem>> _options;
        private readonly Dictionary<TItem, DoublyLinkedList<IDlOption<TItem>>> _items;

        public IEnumerable<IDlOption<TItem>> Options => _options.Values;
        public IEnumerable<TItem> Items => _items.Keys;

        public DancingLinksPlatform()
        {
            _options = new DoublyLinkedList<IDlOption<TItem>>();
            _items = new Dictionary<TItem, DoublyLinkedList<IDlOption<TItem>>>();
        }

        public void AddOption(IDlOption<TItem> option)
        {
            _options.AddValue(option);

            option.Items.ToList()
                .ForEach(item =>
                {
                    if (!_items.ContainsKey(item))
                        _items[item] = new DoublyLinkedList<IDlOption<TItem>>();

                    _items[item].AddValue(option);
                });
        }
    }
}