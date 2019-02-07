using System;
using System.IO;

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

        public PizzaDescription(string inputFile)
        {
            var reader = new StreamReader(File.Open(inputFile, FileMode.Open));

            ReadSizes(reader.ReadLine());

            Ingredients = new int[Height, Width];

            ReadIngredients(reader);
        }

        private void ReadIngredients(StreamReader reader)
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
    }
}