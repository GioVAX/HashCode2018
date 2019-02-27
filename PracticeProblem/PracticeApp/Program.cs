using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PracticeApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Provide input file!");
                return;
            }

            var inputFile = Path.GetFullPath(args[0]);
            //Console.WriteLine($"Processing file {inputFile}");

            PizzaDescription pizza;
            using (var reader = new StreamReader(File.Open(inputFile, FileMode.Open)))
            {
                pizza = new PizzaDescription(reader);
            }

            var slices = PizzaSlicer.Slice(pizza).ToList();

            Console.WriteLine(FormatOutput(slices));
        }

        public static string FormatOutput(List<Slice> slices) => 
            string.Join('\n',
                slices.Select(sl => $"{sl.TopRow} {sl.LeftCol} {sl.BottomRight.Y} {sl.BottomRight.X}")
                    .Prepend($"{slices.Count}"));
    }
}
