using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeApp
{
    public class Templates
    {
        private List<SliceTemplate>[] _factors;

        public Templates()
        {
            _factors = new List<SliceTemplate>[15];

            _factors[0] = null;
            _factors[1] = null;
            _factors[2] = new List<SliceTemplate> { new SliceTemplate(1, 2), new SliceTemplate(2, 1) };
            _factors[3] = new List<SliceTemplate> { new SliceTemplate(1, 3), new SliceTemplate(3, 1) };
            _factors[4] = new List<SliceTemplate> { new SliceTemplate(1, 4), new SliceTemplate(2, 2), new SliceTemplate(4, 1) };
            _factors[5] = new List<SliceTemplate> { new SliceTemplate(1, 5), new SliceTemplate(5, 1) };
            _factors[6] = new List<SliceTemplate> { new SliceTemplate(1, 6), new SliceTemplate(2, 3), new SliceTemplate(3, 2), new SliceTemplate(6, 1) };
            _factors[7] = new List<SliceTemplate> { new SliceTemplate(1, 7), new SliceTemplate(7, 1) };
            _factors[8] = new List<SliceTemplate> { new SliceTemplate(1, 8), new SliceTemplate(2, 4), new SliceTemplate(4, 2), new SliceTemplate(8, 1) };
            _factors[9] = new List<SliceTemplate> { new SliceTemplate(1, 9), new SliceTemplate(3, 3), new SliceTemplate(9, 1) };
            _factors[10] = new List<SliceTemplate> { new SliceTemplate(1, 10), new SliceTemplate(2, 5), new SliceTemplate(5, 2), new SliceTemplate(10, 1) };
            _factors[11] = new List<SliceTemplate> { new SliceTemplate(1, 11), new SliceTemplate(11, 1) };
            _factors[12] = new List<SliceTemplate> { new SliceTemplate(1, 12), new SliceTemplate(2, 6), new SliceTemplate(3, 4), new SliceTemplate(4, 3), new SliceTemplate(6, 2), new SliceTemplate(12, 1) };
            _factors[13] = new List<SliceTemplate> { new SliceTemplate(1, 13), new SliceTemplate(13, 1) };
            _factors[14] = new List<SliceTemplate> { new SliceTemplate(1, 14), new SliceTemplate(2, 7), new SliceTemplate(7, 2), new SliceTemplate(14, 1) };
        }

        public IEnumerable<SliceTemplate> Generate(int minSize, int maxSize) => 
            _factors
                .Skip(minSize)
                .Take(maxSize - minSize + 1)
                .SelectMany(f => f);
    }
}