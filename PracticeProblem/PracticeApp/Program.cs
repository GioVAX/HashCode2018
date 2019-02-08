using System;
using System.IO;
using System.Linq;

namespace PracticeApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Provide input file!");
                return;
            }

            var inputFile = Path.GetFullPath(args[0]);
            Console.WriteLine($"Processing file {inputFile}");

            var pizza = new PizzaDescription(inputFile);
            var slices = PizzaSlicer.Slice(pizza).ToList();
            var size = slices.Sum(s => s.Size);

        }
    }
}
