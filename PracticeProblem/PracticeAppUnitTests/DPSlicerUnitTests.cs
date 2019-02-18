using System.IO;
using System.Linq;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    // ReSharper disable once InconsistentNaming
    public class DPSlicerUnitTests
    {
        private readonly DPPizzaSlicer _sut;
        private readonly PizzaDescription _pizza;

        public DPSlicerUnitTests()
        {
            _pizza = new PizzaDescription(PizzaCases.Example);
            _sut = new DPPizzaSlicer(_pizza);
        }

        [Theory]
        [InlineData("3 3 1 3\nTTT\nTMM\nTTT", 6)]
        [InlineData("3 5 1 6\nTTTTT\nTMMMT\nTTTTT", 15)]
        [InlineData("6 7 1 5\nTMMMTTT\nMMMMTMM\nTTMTTMT\nTMMTMMM\nTTTTTTM\nTTTTTTM", 42)]
        public void Solve_ShouldReturnExpectedCoverage(string pizza, int expected)
        {
            var slicer = InitializeSlicer(pizza);

            slicer.Solve()
                .Should().Be(expected);
        }

        private static DPPizzaSlicer InitializeSlicer(string pizza)
        {
            var pizzaDesc = new PizzaDescription(new StringReader(pizza));
            return new DPPizzaSlicer(pizzaDesc);
        }

        [Theory]
        [InlineData("3 3 1 3\nTTT\nTMM\nTTT")]
        [InlineData("3 5 1 6\nTTTTT\nTMMMT\nTTTTT")]
        [InlineData("6 7 1 5\nTMMMTTT\nMMMMTMM\nTTMTTMT\nTMMTMMM\nTTTTTTM\nTTTTTTM")]
        public void GenerateSolutionSpace_ShouldReturnCorrectData(string pizza)
        {
            var sut = InitializeSlicer(pizza);

            _sut.SolutionSpace.Rank
                .Should().Be(2);

            _sut.SolutionSpace.GetLength(0)
                .Should().Be(_pizza.Height);

            _sut.SolutionSpace.GetLength(1)
                .Should().Be(_pizza.Width);
        }

        [Fact]
        public void SliceMediumPizza_ShouldReturn50K()
        {
            var input = new StreamReader( File.Open(@"..\..\..\..\c_medium.in", FileMode.Open));
            var pizza = new PizzaDescription(input);
            var slicer = new DPPizzaSlicer(pizza);

            slicer.Solve()
                .Should().Be(pizza.Width * pizza.Height);
        }
    }
}
