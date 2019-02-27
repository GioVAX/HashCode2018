using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeApp
{
    public class PizzaSlicer
    {
        public static List<Slice>[] CreateSliceDistribution(int width, int height, IEnumerable<Slice> validSlices)
        {
            var possibleSlices = new List<Slice>[width * height];

            foreach (var validSlice in validSlices)
                foreach (var index in validSlice.MapToArray(width))
                    (possibleSlices[index] ?? (possibleSlices[index] = new List<Slice>())).Add(validSlice);

            return possibleSlices;
        }

        public static IEnumerable<Slice> Slice(PizzaDescription pizza)
        {
            var possibleSlices = CreateSliceDistribution(pizza.Width, pizza.Height, pizza.ValidSlices);

            // Greedy approach
            var greedy = SortedApproach(possibleSlices, pizza.Width, ls => ls.OrderByDescending(s => s.Size).ToList())
                .ToList();

            var cover = greedy.Sum(slice => slice.Size);
            if (cover == pizza.Width * pizza.Height)
                return greedy;

            // Small slices first
            var unGreedy = SortedApproach(possibleSlices, pizza.Width, ls => ls.OrderBy(s => s.Size).ToList())
                .ToList();

            var cover2 = unGreedy.Sum(slice => slice.Size);

            return cover > cover2 ? greedy : unGreedy;
        }

        public static IEnumerable<Slice> SortedApproach(List<Slice>[] possibleSlices, int width, Func<List<Slice>, List<Slice>> sorter)
        {
            var sortedPossibleSlices = possibleSlices
                .Select(ls => ls == null ? new List<Slice>() : sorter(ls))
                .ToArray();

            foreach (var slices in sortedPossibleSlices)
            {
                if ((slices?.Count() ?? 0) == 0)
                    continue;

                var maxSlice = slices.First();
                EmptySliceSpace(sortedPossibleSlices, maxSlice, width);

                yield return maxSlice;
            }
        }

        public static void EmptySliceSpace(List<Slice>[] slices, Slice remove, int width)
        {
            foreach (var idx in remove.MapToArray(width))
            {
                var copy = new Slice[slices[idx].Count];
                slices[idx].CopyTo(copy);
                foreach (var slice in copy)
                    RemoveSlice(slices, slice, width);
            }
        }

        public static void RemoveSlice(List<Slice>[] sortedPossibleSlices, Slice slice, int width)
        {
            foreach (var idx in slice.MapToArray(width))
                sortedPossibleSlices[idx].Remove(slice);
        }
    }
}