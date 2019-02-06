using System.Drawing;

namespace PracticeApp
{
    public class Slice
    {
        public Point Origin { get; private set; }

        private readonly SliceTemplate _template;
        public int Width => _template.Width;
        public int Height => _template.Height;

        public Slice(Point origin, SliceTemplate template)
        {
            Origin = origin;
            _template = template;
        }
    }
}
