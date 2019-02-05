namespace PracticeApp
{
    public class SliceTemplate
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SliceTemplate(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}