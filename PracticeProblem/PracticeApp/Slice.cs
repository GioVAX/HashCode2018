using System.Collections.Generic;
using System.Drawing;

namespace PracticeApp
{
    public class Slice
    {
        public int TopRow { get; }
        public int LeftCol { get; }

        public int Width { get; }
        public int Height { get; }

        public int Size => Width * Height;

        public Slice(Point origin, Size size)
        {
            TopRow = origin.Y;
            LeftCol = origin.X;
            Width = size.Width;
            Height = size.Height;
        }

        public IEnumerable<int> MapToArray(int arrayWidth)
        {
            for (var row = 0; row < Height; ++row)
                for (var col = 0; col < Width; ++col)
                {
                    yield return ((TopRow + row) * arrayWidth) + (LeftCol + col);
                }
        }
    }
}
