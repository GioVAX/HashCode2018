using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DLSolution<TItem>
    {
        private readonly Stack<CoverResult<TItem>> _coveredItems;
        public IEnumerable<IDlOption<TItem>> Items => _coveredItems.Select(cr => cr.Option);

        public int Attempts { get; private set; }

        public DLSolution()
        {
            _coveredItems = new Stack<CoverResult<TItem>>();
            Attempts = 0;
        }

        public void AddStep(CoverResult<TItem> result)
        {
            _coveredItems.Push(result);
            ++Attempts;
        }

        public CoverResult<TItem> TraceBack() => _coveredItems.Pop();
    }
}