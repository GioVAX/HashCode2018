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
            var topRow = slice.Origin.X;
            var leftCol = slice.Origin.Y;
            var bottomRow = topRow + slice.Height - 1;
            var rightCol = leftCol + slice.Width - 1;

            if (topRow < 0 || leftCol < 0 || bottomRow >= _width || rightCol >= _height)
                return false;

            var baseValue = _sumValues[bottomRow, rightCol];
            if (topRow != 0)
                baseValue -= _sumValues[topRow - 1, rightCol];

            if (leftCol != 0)
                baseValue -= _sumValues[bottomRow, leftCol - 1];

            if (topRow != 0 && leftCol != 0)
                baseValue += _sumValues[topRow - 1, leftCol - 1];

            return Math.Abs(baseValue) <= (slice.Height * slice.Width - _minSize);
        }
    }
}