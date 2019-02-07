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
                {1,  1,  1,  1, 1},
                {1, -1, -1, -1, 1},
                {1,  1,  1,  1, 1}
            };

            _sut = new SliceValidator(ingredients, 2);
        }
        private bool CheckSlice(int row, int col, int width, int height)
        {
            var slice = new Slice(new Point(row, col), new SliceTemplate(width, height));
            return _sut.IsSliceValid(slice);
        }

        [Theory]
        [InlineData(-1, 0, 2, 1)]
        [InlineData(0, -1, 2, 1)]
        [InlineData(1, 1, 1, 8)]
        [InlineData(1, 1, 12, 2)]
        [InlineData(-10, -10, 120, 200)]
        public void ValidatingSliceOutsideOfPizza_ShouldReturnFalse(int row, int col, int width, int height) =>
            CheckSlice(row, col, width, height).Should().BeFalse();

        [Theory]
        [InlineData(0, 0, 2, 1)]
        [InlineData(0, 0, 1, 3)]
        [InlineData(2, 1, 1, 2)]
        public void WhenCheckingAnInvalidSlice_ShouldReturnFalse(int row, int col, int width, int height) =>
            CheckSlice(row, col, width, height).Should().BeFalse();

        [Theory]
        [InlineData(1, 0, 1, 2)]
        [InlineData(1, 0, 1, 3)]
        [InlineData(0, 0, 2, 3)]
        public void WhenCheckingAValidSlice_ShouldReturnTrue(int row, int col, int width, int height) =>
            CheckSlice(row, col, width, height).Should().BeTrue();
    }
}
