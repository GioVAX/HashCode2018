using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace PracticeApp
{
    // ReSharper disable once InconsistentNaming
    public class DPPizzaSlicer
    {
        private readonly PizzaDescription _pizza;

        public DPPizzaSlicer(PizzaDescription pizza)
        {
            _pizza = pizza;
            _slices = new Slice[_pizza.Height + 1, _pizza.Width + 1];
        }

        private int[,] _solutionSpace;

        public int[,] SolutionSpace =>
            _solutionSpace ?? (_solutionSpace = new int[_pizza.Height + 1, _pizza.Width + 1]);

        private Slice[,] _slices;

        public int Solve()
        {
            var slices = _pizza.ValidSlices
                .Select(sl => new Slice(new Point(sl.LeftCol + 1, sl.TopRow + 1), new Size(sl.Width, sl.Height)))
                .ToList();

            BuildSolutionSpace(slices);

            return SolutionSpace[_pizza.Height, _pizza.Width];
        }

        private void BuildSolutionSpace(List<Slice> slices)
        {
            var slicesCount = slices.Count;
            for (var col = 1; col <= _pizza.Width; ++col)
                for (var row = 1; row <= _pizza.Height; ++row)
                {
                    InitSolutionAt(row, col);

                    for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex)
                    {
                        var slice = slices[sliceIndex];
                        if (slice.BottomRight.X > col || slice.BottomRight.Y > row)
                            continue;

                        if (row < slice.TopRow
                            || row > slice.BottomRight.Y
                            || col < slice.LeftCol
                            || col > slice.BottomRight.X)
                            continue;

                        var bottomRight = slice.BottomRight;

                        var valueWith = 0;

                        if (bottomRight.X <= col && bottomRight.Y <= row)
                        {
                            var sectionUp = SolutionSpace[slice.TopRow - 1, col];
                            var sectionLeft = SolutionSpace[row, slice.LeftCol - 1];

                            valueWith = slice.Size
                                        + sectionUp
                                        + sectionLeft;
                        }

                        if (valueWith <= SolutionSpace[row, col])
                            continue;

                        SolutionSpace[row, col] = valueWith;
                        _slices[row, col] = slice;
                    }
                }
        }

        private void InitSolutionAt(int row, int col)
        {
            if (SolutionSpace[row - 1, col] > SolutionSpace[row, col - 1])
            {
                SolutionSpace[row, col] = SolutionSpace[row - 1, col];
                _slices[row, col] = _slices[row - 1, col];
            }
            else
            {
                SolutionSpace[row, col] = SolutionSpace[row, col - 1];
                _slices[row, col] = _slices[row, col - 1];
            }
        }
    }
}