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
            _sut = new PizzaDescription(@"..\..\..\..\a_example.in");
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
                    { 1, 1, 1, 1, 1 },
                    { 1, -1, -1, -1, 1 },
                    { 1, 1, 1, 1, 1 }
                });
        }

        [Fact]
        public void WhenUsingSmallInput_ShouldReturnTheSmallPizza()
        {
            var pizza = new PizzaDescription(@"..\..\..\..\b_small.in");

            pizza.Width.Should().Be(7);
            pizza.Height.Should().Be(6);
            pizza.MinSlice.Should().Be(2);
            pizza.MaxSlice.Should().Be(5);

            pizza.Ingredients.Should()
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
