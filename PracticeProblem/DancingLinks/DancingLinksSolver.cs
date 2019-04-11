using System;
using System.Collections.Generic;
using System.Linq;

namespace DancingLinks
{
    public class DancingLinksSolver<TItem> where TItem : IComparable
    {
        private readonly DancingLinksPlatform<TItem> _platform;

        public DancingLinksSolver()
        {
            _platform = new DancingLinksPlatform<TItem>();
        }

        public void AddOption(IDlOption<TItem> option) => _platform.AddOption(option);
        public void AddItem(TItem item) => _platform.AddItem(item);

        public IEnumerable<IDlOption<TItem>> Solve()
        {
            var candidateSolution = new Stack<CoverResult<TItem>>();

            var triedSolutions = new List<CoverResult<TItem>>();

            while (true)
            {
                var option = SelectOptionToCover(_platform, triedSolutions.Select(sol => sol.Option));
                if (option != null)
                    candidateSolution.Push(_platform.Cover(option));
                else
                {
                    if (AcceptableSolution(candidateSolution, _platform) || candidateSolution.Count == 0)
                        break; // Solution found or no solution is possible

                    triedSolutions.Add(candidateSolution.Pop());
                }
            }

            return candidateSolution.Select(cr => cr.Option);
        }

        private static bool AcceptableSolution(Stack<CoverResult<TItem>> candidateSolution, DancingLinksPlatform<TItem> platform) => !platform.Items.Any();

        private static IDlOption<TItem> SelectOptionToCover(DancingLinksPlatform<TItem> platform, IEnumerable<IDlOption<TItem>> tried)
        {
            var item = platform.ItemHeaders.OrderBy(hdr => hdr.Options.Count).FirstOrDefault();

            return item?.Options
                .SkipWhile(tried.Contains)
                .FirstOrDefault();
        }
    }
}