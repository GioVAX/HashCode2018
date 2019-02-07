using System.Drawing;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class SliceValidatorUnitTests
    {
        private readonly SliceValidator _sut;

        public SliceValidatorUnitTests()
        {
            var ingredients = new[,]
            {
                {1, 1, 1, 1, 1},
                {1, -1, -1, -1, 1},
                {1, 1, 1, 1, 1}
            };

            _sut = new SliceValidator(ingredients);
        }
        private bool CheckSlice(int topX, int topY, int width, int height, int tolerance)
        {
            var slice = new Slice(new Point(topX, topY), new SliceTemplate(width, height));
            return _sut.IsSliceValid(slice, tolerance);
        }

        [Theory]
        [InlineData(-1, 0, 2, 1)]
        [InlineData(0, -1, 2, 1)]
        public void ValidatingSliceOutsideOfPizza_ShouldReturnFalse(int topX, int topY, int width, int height) =>
            CheckSlice(topX, topY, width, height, 0).Should().BeFalse();



        [Theory]
        [InlineData(0, 0, 2, 1, 0)]
        [InlineData(0, 0, 1, 3, 1)]
        [InlineData(2, 1, 1, 2, 1)]
        public void WhenCheckingAnInvalidSlice_ShouldReturnFalse(int topX, int topY, int width, int height, int tolerance) =>
            CheckSlice(topX, topY, width, height, tolerance).Should().BeFalse();

        [Theory]
        [InlineData(1, 0, 1, 2, 0)]
        [InlineData(1, 0, 1, 3, 1)]
        [InlineData(0, 0, 2, 3, 4)]
        public void WhenCheckingAValidSlice_ShouldReturnTrue(int topX, int topY, int width, int height, int tolerance) =>
            CheckSlice(topX, topY, width, height, tolerance).Should().BeTrue();
    }
}
