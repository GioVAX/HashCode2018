using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PracticeApp
{
    public class Slice
    {
        public int TopRow { get; }
        public int LeftCol { get; }

        public int Width { get; }
        public int Height { get; }

        public int Size => Width * Height;

        public Point TopLeft => new Point(LeftCol, TopRow);
        public Point BottomRight => new Point(LeftCol + Width - 1, TopRow + Height - 1);

        private IEnumerable<Point> _points;

        public Slice(Point origin, Size size)
        {
            TopRow = origin.Y;
            LeftCol = origin.X;
            Width = size.Width;
            Height = size.Height;
        }

        public IEnumerable<Point> Points =>
            _points ?? (_points = Enumerable.Range(TopRow, Height)
                .SelectMany(row => Enumerable.Range(LeftCol, Width).Select(col => new Point(col, row))));

        public IEnumerable<int> MapToArray(int arrayWidth) =>
            Points.Select(pnt => pnt.X + (pnt.Y * arrayWidth));
    }
}
