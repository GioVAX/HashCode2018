using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class ExamplePizzaUnitTests
    {
        private PizzaDescription _sut;

        public ExamplePizzaUnitTests()
        {
            using (var reader = new StreamReader(File.Open(@"..\..\..\..\a_example.in", FileMode.Open)))
            {
                _sut = new PizzaDescription(reader);
            }
        }

        [Fact]
        public void Sut_ShouldContainTheExamplePizza()
        {

            _sut.Width.Should().Be(5);
            _sut.Height.Should().Be(3);
            _sut.MinSlice.Should().Be(2);
            _sut.MaxSlice.Should().Be(6);

            _sut.Ingredients.Should()
                .BeEquivalentTo(new[,] {
                    { 1,  1,  1,  1, 1 },
                    { 1, -1, -1, -1, 1 },
                    { 1,  1,  1,  1, 1 }
                });
        }

        [Fact]
        public void ShouldGenerateAllPossibleSlices()
        {
            _sut.AllSlices.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount(13 * _sut.Width * _sut.Height);
        }

        [Fact]
        public void ShouldListValidSlices()
        {
            _sut.ValidSlices.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount(34);
        }
    }
}
