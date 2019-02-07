using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace PracticeApp
{
    public class SlicesGenerator
    {
        public IEnumerable<Slice> GenerateAllSlices(int pizzaWidth, int pizzaHeight, IEnumerable<Size> sizes)
        {
            for (var row = 0; row < pizzaHeight; ++row)
                for( var col = 0; col < pizzaWidth; ++col)
                    foreach (var size in sizes)
                        yield return new Slice(new Point(col, row), size);
        }
    }
}
