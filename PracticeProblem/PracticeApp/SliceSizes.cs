using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PracticeApp
{
    public class SliceSizes
    {
        private readonly List<Size>[] _factors;

        public SliceSizes()
        {
            _factors = new List<Size>[15];

            _factors[0] = null;
            _factors[1] = null;
            _factors[2] = new List<Size> { new Size(1, 2), new Size(2, 1) };
            _factors[3] = new List<Size> { new Size(1, 3), new Size(3, 1) };
            _factors[4] = new List<Size> { new Size(1, 4), new Size(2, 2), new Size(4, 1) };
            _factors[5] = new List<Size> { new Size(1, 5), new Size(5, 1) };
            _factors[6] = new List<Size> { new Size(1, 6), new Size(2, 3), new Size(3, 2), new Size(6, 1) };
            _factors[7] = new List<Size> { new Size(1, 7), new Size(7, 1) };
            _factors[8] = new List<Size> { new Size(1, 8), new Size(2, 4), new Size(4, 2), new Size(8, 1) };
            _factors[9] = new List<Size> { new Size(1, 9), new Size(3, 3), new Size(9, 1) };
            _factors[10] = new List<Size> { new Size(1, 10), new Size(2, 5), new Size(5, 2), new Size(10, 1) };
            _factors[11] = new List<Size> { new Size(1, 11), new Size(11, 1) };
            _factors[12] = new List<Size> { new Size(1, 12), new Size(2, 6), new Size(3, 4), new Size(4, 3), new Size(6, 2), new Size(12, 1) };
            _factors[13] = new List<Size> { new Size(1, 13), new Size(13, 1) };
            _factors[14] = new List<Size> { new Size(1, 14), new Size(2, 7), new Size(7, 2), new Size(14, 1) };
        }

        public IEnumerable<Size> Generate(int minSize, int maxSize) =>
            _factors
                .Skip(minSize)
                .Take(maxSize - minSize + 1)
                .SelectMany(f => f);
    }
}