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
        private DPPizzaSlicer _sut;
        private PizzaDescription _pizza;

        public DPSlicerUnitTests() => InitializeSlicer(PizzaCases.Example);

        private void InitializeSlicer(string pizza) => InitializeSlicer(new StringReader(pizza));

        private void InitializeSlicer(TextReader pizzaRdr)
        {
            _pizza = new PizzaDescription(pizzaRdr);
            _sut = new DPPizzaSlicer(_pizza);
        }

        [Theory]
        [InlineData("3 3 1 3\nTTT\nTMM\nTTT", 2)]
        [InlineData("3 5 1 6\nTTTTT\nTMMMT\nTTTTT", 3)]
        [InlineData("6 7 1 5\nTMMMTTT\nMMMMTMM\nTTMTTMT\nTMMTMMM\nTTTTTTM\nTTTTTTM", 9)]
        public void Solve_ShouldReturnListOfSlices(string pizza, int expectedSlices)
        {
            InitializeSlicer(pizza);

            _sut.Solve().Count()
                .Should().Be(expectedSlices);
        }

        [Theory]
        [InlineData("3 3 1 3\nTTT\nTMM\nTTT", 6)]
        [InlineData("3 5 1 6\nTTTTT\nTMMMT\nTTTTT", 15)]
        [InlineData("6 7 1 5\nTMMMTTT\nMMMMTMM\nTTMTTMT\nTMMTMMM\nTTTTTTM\nTTTTTTM", 42)]
        public void Solve_ShouldReturnExpectedCoverage(string pizza, int expected)
        {
            InitializeSlicer(pizza);

            _sut.Solve()
                .Sum(sl => sl.Size)
                .Should().Be(expected);
        }

        //[Theory]
        //[InlineData("3 3 1 3\nTTT\nTMM\nTTT")]
        //[InlineData("3 5 1 6\nTTTTT\nTMMMT\nTTTTT")]
        //[InlineData("6 7 1 5\nTMMMTTT\nMMMMTMM\nTTMTTMT\nTMMTMMM\nTTTTTTM\nTTTTTTM")]
        //public void GenerateSolutionSpace_ShouldReturnCorrectData(string pizza)
        //{
        //    InitializeSlicer(pizza);

        //    _sut.SolutionSpace.Rank
        //        .Should().Be(2);

        //    _sut.SolutionSpace.GetLength(0)
        //        .Should().Be(_pizza.Height + 1);

        //    _sut.SolutionSpace.GetLength(1)
        //        .Should().Be(_pizza.Width + 1);
        //}

        //[Fact]
        //public void SliceExamplePizza_ShouldReturnCorrect3Slices()
        //{
        //    InitializeSlicer(PizzaCases.Example);

        //    var slices = _sut.Solve();
        //}

        [Fact(Skip = "On Hold")]
        public void SliceMediumPizza_ShouldReturn50K()
        {
            var input = new StreamReader(File.Open(@"..\..\..\..\c_medium.in", FileMode.Open));
            InitializeSlicer(input);

            _sut.Solve()
                .Sum(sl => sl.Size)
                .Should().Be(_pizza.Width * _pizza.Height);
        }
    }
}
