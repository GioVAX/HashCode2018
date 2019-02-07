using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class TinyPizzaUnitTests
    {
        private readonly PizzaDescription _sut;

        public TinyPizzaUnitTests()
        {
            _sut = new PizzaDescription(@"..\..\..\..\tiny.in");
        }

        [Fact]
        public void Sut_ShouldContainTheTinyPizza()
        {

            _sut.Width.Should().Be(3);
            _sut.Height.Should().Be(3);
            _sut.MinSlice.Should().Be(2);
            _sut.MaxSlice.Should().Be(3);

            _sut.Ingredients.Should()
                .BeEquivalentTo(new[,] {
                    { 1,  1,  1 },
                    { 1, -1, -1 },
                    { 1,  1,  1 }
                });
        }

        [Fact]
        public void ShouldGenerateAllPossibleSlices()
        {
            _sut.AllSlices.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount(4 * _sut.Width * _sut.Height);
        }

        [Fact]
        public void ShouldListValidSlices()
        {
            var actual = _sut.ValidSlices;

            actual.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.OnlyHaveUniqueItems()
                .And.BeEquivalentTo(
                    new List<Slice>
                    {
                        new Slice( new Point(0, 1), new Size( 2, 1) ),
                        new Slice( new Point(0, 1), new Size( 3, 1) ),

                        new Slice( new Point(1, 0), new Size( 1, 2) ),
                        new Slice( new Point(1, 0), new Size( 1, 3) ),
                        new Slice( new Point(1, 1), new Size( 1, 2) ),

                        new Slice( new Point(2, 0), new Size( 1, 2) ),
                        new Slice( new Point(2, 0), new Size( 1, 3) ),
                        new Slice( new Point(2, 1), new Size( 1, 2) ),
                    }
                );
        }

    }
}
