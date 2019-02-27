using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class ProgramUnitTests
    {
        [Fact]
        public void FormatOutputWithSpecificSlices_ShouldReturnCorrectString()
        {
            var slices = new List<Slice>();
            slices.Add(new Slice(new Point(1, 1), new Size(3, 4)));

            var output = Program.FormatOutput(slices);

            output.Should()
                .Be("1\n1 1 4 3");
        }

        [Fact]
        public void FormatOutputForExamplePizza_ShouldReturnCorrectString()
        {
            var slices = new List<Slice>();
            slices.Add(new Slice(new Point(0, 0), new Size(2, 3)));
            slices.Add(new Slice(new Point(2, 0), new Size(1, 3)));
            slices.Add(new Slice(new Point(3, 0), new Size(2, 3)));

            var output = Program.FormatOutput(slices);

            output.Should()
                .Be("3\n0 0 2 1\n0 2 2 2\n0 3 2 4");
        }
    }
}
