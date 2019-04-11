using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class CoverResult<TItem>
    {
        private readonly Stack<RemovedNodeWrapper<ItemHeader<TItem>>> _itemsRemoved;
        private readonly Stack<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>> _itemOptionsRemoved;
        
        public IDlOption<TItem> Option { get; }
        public IEnumerable<TItem> Items => _itemsRemoved.Select(node => node.Value.Value.Item);

        public CoverResult( IDlOption<TItem> option)
        {
            _itemsRemoved = new Stack<RemovedNodeWrapper<ItemHeader<TItem>>>();
            _itemOptionsRemoved = new Stack<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>>();
            Option = option;
        }

        public void AddItem(RemovedNodeWrapper<ItemHeader<TItem>> item) => _itemsRemoved.Push(item);

        public void AddOptionNode(Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>> tuple) =>
            _itemOptionsRemoved.Push(tuple);

        public IEnumerable<RemovedNodeWrapper<ItemHeader<TItem>>> CoveredItems
        {
            get
            {
                while (_itemsRemoved.TryPop(out var item))
                    yield return item;
            }
        }

        public IEnumerable<Tuple<TItem, RemovedNodeWrapper<IDlOption<TItem>>>> CoveredOptions
        {
            get
            {
                while (_itemOptionsRemoved.TryPop(out var tuple))
                    yield return tuple;
            }
        }
    }
}
