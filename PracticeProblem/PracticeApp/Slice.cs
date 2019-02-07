using System.Drawing;

namespace PracticeApp
{
    public class Slice
    {
        public int TopRow { get; }
        public int LeftCol { get; }

        public int Width { get; }
        public int Height { get; }

        public Slice(Point origin, Size size)
        {
            TopRow = origin.Y;
            LeftCol = origin.X;
            Width = size.Width;
            Height = size.Height;
        }
    }
}
