using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DancingLinksPlatform<TItem> where TItem : IComparable
    {
        private readonly DoublyLinkedList<IDlOption<TItem>> _options;
        private readonly DoublyLinkedList<DoublyLinkedList<IDlOption<TItem>>> _items;
        private readonly Dictionary<TItem, DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> _itemsIndex;

        public IEnumerable<IDlOption<TItem>> Options => _options.Values;
        public int ItemsCount => _items.Values.Count();

        public DancingLinksPlatform()
        {
            _options = new DoublyLinkedList<IDlOption<TItem>>();
            _items = new DoublyLinkedList<DoublyLinkedList<IDlOption<TItem>>>();
            _itemsIndex = new Dictionary<TItem, DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>>();
        }

        public void AddOption(IDlOption<TItem> option)
        {
            _options.AddValue(option);

            option.Items.ToList()
                .ForEach(item =>
                {
                    if (!_itemsIndex.ContainsKey(item))
                        _itemsIndex[item] = _items.AddValue(new DoublyLinkedList<IDlOption<TItem>>());

                    _itemsIndex[item].Value.AddValue(option);
                });
        }

        public CoverResult<TItem> Cover(IDlOption<TItem> option)
        {
            var stack = new Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>>();

            var heads = GetOptionColumns(option)
                .Select(columnHead => _items.RemoveNode(columnHead))
                .ToList();

            var options = FindOverlappingOptions(heads.Select(head => head.Value))
                .Where(opt => opt != option);

            heads.ForEach(stack.Push);

            return new CoverResult<TItem>(option, stack, options);
        }

        private IEnumerable<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> GetOptionColumns(IDlOption<TItem> option) =>
            option.Items
                .Select(item => _itemsIndex[item]);

        public IEnumerable<IDlOption<TItem>> FindOverlappingOptions(IDlOption<TItem> option) =>
            FindOverlappingOptions(GetOptionColumns(option).Select(head => head.Value));

        private static IEnumerable<IDlOption<TItem>> FindOverlappingOptions(
            IEnumerable<DoublyLinkedList<IDlOption<TItem>>> columns) =>
            columns
                .SelectMany(list => list.Values)
                .Distinct();
    }
}