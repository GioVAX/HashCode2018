using System;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class PizzaUnitTests
    {
        [Fact]
        public void UsingExampleInput_GivesTheExamplePizza()
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

        [Fact]
        public void UsingSmallInput_GivesTheSmallPizza()
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
