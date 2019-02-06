using System;

namespace PracticeApp
{
    public class SliceValidator
    {
        private readonly int[,] _sumValues;

        public SliceValidator(int[,] values)
        {
            var w = values.GetLength(0);
            var h = values.GetLength(1);
            _sumValues = new int[w, h];

            var rowSum = 0;
            for (var i = 0; i < w; ++i)
                _sumValues[i, 0] = (rowSum += values[i, 0]);

            for (var i = 1; i < h; ++i)
            {
                rowSum = 0;
                for (var j = 0; j < w; ++j)
                    _sumValues[j, i] = (rowSum += values[j, i]) + _sumValues[j, i - 1];
            }
        }

        public bool IsSliceValid(Slice slice, int maxValue)
        {
            // Assumption: slice will always be fully contained in the array!

            var leftX = slice.Origin.X;
            var topY = slice.Origin.Y;
            var rightX = leftX + slice.Width -1;
            var bottomY = topY + slice.Height-1;

            var baseValue = _sumValues[rightX, bottomY];
            if (leftX != 0)
                baseValue -= _sumValues[leftX - 1, bottomY];

            if (topY != 0)
                baseValue -= _sumValues[rightX, topY - 1];

            if (leftX != 0 && topY != 0)
                baseValue += _sumValues[leftX - 1, topY - 1];

            return Math.Abs(baseValue) <= maxValue;
        }
    }
}