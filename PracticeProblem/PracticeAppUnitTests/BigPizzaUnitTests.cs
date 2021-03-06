using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class BigPizzaUnitTests
    {
        private readonly PizzaDescription _sut;

        public BigPizzaUnitTests()
        {
            using (var reader = new StreamReader(File.Open(@"..\..\..\..\d_big.in", FileMode.Open)))
            {
                _sut = new PizzaDescription(reader);
            }
        }

        [Fact]
        public void Sut_ShouldContainTheBigPizza()
        {

            _sut.Width.Should().Be(1000);
            _sut.Height.Should().Be(1000);
            _sut.MinSlice.Should().Be(12);
            _sut.MaxSlice.Should().Be(14);

            _sut.Ingredients.Should()
                .HaveCount( 1000000);
        }

        [Fact]
        public void ShouldGenerateAllPossibleSlices()
        {
            _sut.AllSlices.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount(12 * _sut.Width * _sut.Height);
        }

        [Fact]
        public void ShouldListValidSlices()
        {
            var actual = _sut.ValidSlices;

            actual.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount(4454943);
        }
    }
}
