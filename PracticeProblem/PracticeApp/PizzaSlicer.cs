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

            return GreedyApproach(possibleSlices, pizza.Width);
        }


        public static IEnumerable<Slice> GreedyApproach(List<Slice>[] possibleSlices, int width)
        {
            var sortedPossibleSlices = possibleSlices
                .Select(ls => ls == null ? new List<Slice>() : ls.OrderByDescending(s => s.Size).ToList())
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