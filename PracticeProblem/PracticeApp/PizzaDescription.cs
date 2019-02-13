using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PracticeApp
{
    public class PizzaDescription
    {
        private char MUSHROOM = 'M';
        private char TOMATO = 'T';

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MaxSlice { get; private set; }
        public int MinSlice { get; private set; }

        public int[,] Ingredients { get; }

        public PizzaDescription(TextReader reader)
        {
            ReadSizes(reader.ReadLine());

            Ingredients = new int[Height, Width];

            ReadIngredients(reader);
        }

        private void ReadIngredients(TextReader reader)
        {
            for (var r = 0; r < Height; ++r)
            {
                var line = reader.ReadLine();
                for (var c = 0; c < Width; ++c)
                    Ingredients[r, c] = line[c] == TOMATO ? 1 : -1;
            }
        }

        private void ReadSizes(string line)
        {
            var sizes = line.Split(' ');

            Height = Convert.ToInt32(sizes[0]);
            Width = Convert.ToInt32(sizes[1]);
            MinSlice = Convert.ToInt32(sizes[2]) * 2;
            MaxSlice = Convert.ToInt32(sizes[3]);
        }

        public IEnumerable<Slice> ValidSlices
        {
            get
            {
                var validator = new SliceValidator(Ingredients, MinSlice, MaxSlice);

                return AllSlices
                    .Where(s => validator.IsSliceValid(s));
            }
        }

        public IEnumerable<Slice> AllSlices
        {
            get
            {
                var sizes = new SliceSizes().Generate(MinSlice, MaxSlice).ToArray();

                for (var row = 0; row < Height; ++row)
                    for (var col = 0; col < Width; ++col)
                        foreach (var size in sizes)
                            yield return new Slice(new Point(col, row), size);
            }
        }
    }
}