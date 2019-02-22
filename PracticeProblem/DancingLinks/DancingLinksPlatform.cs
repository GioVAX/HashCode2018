﻿using System;
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
                    {
                        _itemsIndex[item] = _items.AddValue(new DoublyLinkedList<IDlOption<TItem>>());
                    }

                    _itemsIndex[item].Value.AddValue(option);
                });
        }

        public Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>> Cover(IDlOption<TItem> option)
        {
            var stack = new Stack<DoublyLinkedListNode<DoublyLinkedList<IDlOption<TItem>>>>();

            option.Items
                .Select(item => _itemsIndex[item])
                .Select(columnHead => _items.RemoveNode(columnHead))
                .ToList()
                .ForEach(stack.Push);

            return stack;

            //option.Items.Aggregate(new Stack<DoublyLinkedListNode<DoublyLinkedListOfItems>>(),
            //    (stack, item) =>
            //    {
            //        var head = _itemsIndex[item];
            //        _items.RemoveNode(head);
            //        stack.Push(head);
            //        return stack;
            //    });
        }
    }
}