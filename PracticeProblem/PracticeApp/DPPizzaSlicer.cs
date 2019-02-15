using System;
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
        }

        private int[,,] _solutionSpace;

        public int[,,] SolutionSpace =>
            _solutionSpace ?? (_solutionSpace = new int[_pizza.Height, _pizza.Width, _pizza.ValidSlices.Count() + 1]);

        public int Solve()
        {
            var slices = _pizza.ValidSlices.ToList();
            var slicesCount = slices.Count;

            for (var col = 0; col < _pizza.Width; ++col)
                for (var row = 0; row < _pizza.Height; ++row)
                {
                    SolutionSpace[row,col,slicesCount] = Math.Max(
                        row == 0 ? 0 : SolutionSpace[row - 1, col, slicesCount],
                        col == 0 ? 0 : SolutionSpace[row, col - 1, slicesCount]);

                    for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex)
                    {
                        var slice = slices[sliceIndex];
                        if (slice.BottomRight.X > col || slice.BottomRight.Y > row)
                            continue;

                        if (row < slice.TopRow || row > slice.BottomRight.Y
                            || col < slice.LeftCol || col > slice.BottomRight.X)
                            continue;

                        var bottomRight = slice.BottomRight;

                        var valueWith = 0;

                        if (bottomRight.X <= col && bottomRight.Y <= row)
                        {
                            var sectionUp = slice.TopRow == 0
                                ? 0
                                : SolutionSpace[slice.TopRow - 1, col, slicesCount];
                            var sectionLeft = slice.LeftCol == 0
                                ? 0
                                : SolutionSpace[row, slice.LeftCol - 1, slicesCount];
                            var overlappingSection = slice.TopRow == 0 || slice.LeftCol == 0
                                ? 0
                                : SolutionSpace[slice.TopRow - 1, slice.LeftCol - 1, slicesCount];

                            valueWith = slice.Size
                                        + sectionUp
                                        + sectionLeft
                                        - overlappingSection;
                        }

                        SolutionSpace[row, col, sliceIndex] =
                            SolutionSpace[row,col,slicesCount] = Math.Max(SolutionSpace[row,col,slicesCount], valueWith);
                    }
                }

            return SolutionSpace[_pizza.Height - 1, _pizza.Width - 1, slicesCount];
        }
    }
}