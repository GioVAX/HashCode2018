using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class PizzaSlicerUnitTests
    {
        [Fact]
        public void WhenInitialized_ShouldContainMatrixOfValidSlices()
        {
            var pizza = new PizzaDescription(PizzaCases.Tiny);
            var slices = PizzaSlicer.CreateSliceDistribution(pizza.Width, pizza.Height, pizza.ValidSlices);

            slices.Should()
                .NotBeNull();

            slices.Select(ls => ls?.Count ?? 0)
                .Should().BeEquivalentTo(new List<int> { 0, 2, 2, 2, 5, 4, 0, 2, 2 });
        }

        [Fact]
        public void WhenSlicingTinyPizza_ShouldReturn2Slices()
        {
            var pizza = new PizzaDescription(PizzaCases.Tiny);
            var slices = PizzaSlicer.Slice(pizza);

            slices.Should()
                .NotBeNull()
                .And.HaveCount(2);
        }

        [Fact]
        public void RemoveSlice_ShouldRemoveOneSlice()
        {
            var slices = new []
                {new List<Slice>(), new List<Slice>(), new List<Slice>(), new List<Slice>()};

            var slice1 = new Slice(new Point(0, 0), new Size(2, 1));
            slices[0].Add(slice1);
            slices[1].Add(slice1);

            var slice2 = new Slice(new Point(1, 0), new Size(1, 2));
            slices[1].Add(slice2);
            slices[3].Add(slice2);

            var slice3 = new Slice(new Point(0, 1), new Size(2, 1));
            slices[2].Add(slice3);
            slices[3].Add(slice3);

            slices[2].Add(new Slice(new Point(0, 1), new Size(1, 1)));

            PizzaSlicer.RemoveSlice(slices, slice1, 2);

            slices[0].Should().BeEmpty();
            slices[1].Should().HaveCount(1);
            slices[2].Should().HaveCount(2);
            slices[3].Should().HaveCount(2);
        }

        [Fact]
        public void CallingEmptySliceSpace_ShouldRemoveAllSlices()
        {
            var slices = new []
                {new List<Slice>(), new List<Slice>(), new List<Slice>(), new List<Slice>()};

            var slice1 = new Slice(new Point(0, 0), new Size(2, 1));
            slices[0].Add(slice1);
            slices[1].Add(slice1);

            var slice2 = new Slice(new Point(1, 0), new Size(1, 2));
            slices[1].Add(slice2);
            slices[3].Add(slice2);

            var slice3 = new Slice(new Point(0, 1), new Size(2, 1));
            slices[2].Add(slice3);
            slices[3].Add(slice3);

            slices[2].Add(new Slice(new Point(0, 1), new Size(1, 1)));

            PizzaSlicer.EmptySliceSpace(slices, slice1, 2);

            slices[0].Should().BeEmpty();
            slices[1].Should().BeEmpty();
            slices[2].Should().HaveCount(2);
            slices[3].Should().HaveCount(1);
        }
    }
}
