using System;

namespace PracticeApp
{
    public class SliceValidator
    {
        private readonly int[,] _sumValues;
        private readonly int _height;
        private readonly int _width;
        private readonly int _minSize;
        private readonly int _maxSize;

        public SliceValidator(int[,] values, int minSize, int maxSize)
        {
            _height = values.GetLength(0);
            _width = values.GetLength(1);
            _sumValues = new int[_height, _width];

            var rowSum = 0;
            for (var i = 0; i < _height; ++i)
                _sumValues[i, 0] = (rowSum += values[i, 0]);

            for (var i = 1; i < _width; ++i)
            {
                rowSum = 0;
                for (var j = 0; j < _height; ++j)
                    _sumValues[j, i] = (rowSum += values[j, i]) + _sumValues[j, i - 1];
            }

            _minSize = minSize;
            _maxSize = maxSize;
        }

        public bool IsSliceValid(Slice slice)
        {
            var size = slice.Height * slice.Width;
            if (size < _minSize || size > _maxSize)
                return false;

            var topRow = slice.TopRow;
            var leftCol = slice.LeftCol;
            var bottomRow = topRow + slice.Height - 1;
            var rightCol = leftCol + slice.Width - 1;

            if (topRow < 0 || leftCol < 0 || bottomRow >= _height || rightCol >= _width)
                return false;

            var baseValue = _sumValues[bottomRow, rightCol];
            if (topRow != 0)
                baseValue -= _sumValues[topRow - 1, rightCol];

            if (leftCol != 0)
                baseValue -= _sumValues[bottomRow, leftCol - 1];

            if (topRow != 0 && leftCol != 0)
                baseValue += _sumValues[topRow - 1, leftCol - 1];

            return Math.Abs(baseValue) <= (size - _minSize);
        }
    }
}