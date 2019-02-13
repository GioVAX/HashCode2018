using System.IO;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class SmallPizzaUnitTests
    {
        private PizzaDescription _sut;

        public SmallPizzaUnitTests()
        {
            using (var reader = new StreamReader(File.Open(@"..\..\..\..\b_small.in", FileMode.Open)))
            {
                _sut = new PizzaDescription(reader);
            }
        }

        [Fact]
        public void Sut_ShouldContainTheSmallPizza()
        {
            _sut.Width.Should().Be(7);
            _sut.Height.Should().Be(6);
            _sut.MinSlice.Should().Be(2);
            _sut.MaxSlice.Should().Be(5);

            _sut.Ingredients.Should()
                .BeEquivalentTo(new[,] {
                    { 1, -1, -1, -1, 1, 1, 1 },
                    { -1, -1, -1, -1, 1, -1, -1 },
                    { 1, 1, -1, 1, 1, -1, 1 },
                    { 1, -1, -1, 1, -1, -1, -1 },
                    { 1, 1, 1, 1, 1, 1, -1 },
                    { 1, 1, 1, 1, 1, 1, -1 }
                });
        }
    }
}
