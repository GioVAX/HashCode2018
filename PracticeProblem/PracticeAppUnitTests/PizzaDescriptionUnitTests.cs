using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class PizzaDescriptionUnitTests
    {
        [Fact]
        public void Sut_ShouldContainTheExamplePizza()
        {
            var pizza = new PizzaDescription(@"..\..\..\..\a_example.in");

            pizza.Width.Should().Be(5);
            pizza.Height.Should().Be(3);
            pizza.MinSlice.Should().Be(2);
            pizza.MaxSlice.Should().Be(6);

            pizza.Ingredients.Should()
                .BeEquivalentTo(new[,] {
                    { 1, 1, 1, 1, 1 },
                    { 1, -1, -1, -1, 1 },
                    { 1, 1, 1, 1, 1 }
                });
        }
    }
}
