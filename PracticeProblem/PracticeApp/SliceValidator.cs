using System;

namespace PracticeApp
{
    public class SliceValidator
    {
        private readonly int[,] _sumValues;
        private readonly int _width;
        private readonly int _height;
        private readonly int _minSize;

        public SliceValidator(int[,] values, int minSize)
        {
            _width = values.GetLength(0);
            _height = values.GetLength(1);
            _sumValues = new int[_width, _height];

            var rowSum = 0;
            for (var i = 0; i < _width; ++i)
                _sumValues[i, 0] = (rowSum += values[i, 0]);

            for (var i = 1; i < _height; ++i)
            {
                rowSum = 0;
                for (var j = 0; j < _width; ++j)
                    _sumValues[j, i] = (rowSum += values[j, i]) + _sumValues[j, i - 1];
            }

            _minSize = minSize;
        }

        public bool IsSliceValid(Slice slice)
        {
            var leftX = slice.Origin.X;
            var topY = slice.Origin.Y;
            var rightX = leftX + slice.Width - 1;
            var bottomY = topY + slice.Height - 1;

            if (leftX < 0 || topY < 0 || rightX >= _width || bottomY >= _height)
                return false;

            var baseValue = _sumValues[rightX, bottomY];
            if (leftX != 0)
                baseValue -= _sumValues[leftX - 1, bottomY];

            if (topY != 0)
                baseValue -= _sumValues[rightX, topY - 1];

            if (leftX != 0 && topY != 0)
                baseValue += _sumValues[leftX - 1, topY - 1];

            return Math.Abs(baseValue) <= slice.Height * slice.Width - _minSize;
        }
    }
}