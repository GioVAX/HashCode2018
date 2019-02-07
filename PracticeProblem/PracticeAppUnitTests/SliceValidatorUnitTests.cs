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

            _sut = new SliceValidator(ingredients, 2, 6);
        }

        private bool CheckSlice(int row, int col, int width, int height)
        {
            var slice = new Slice(new Point(col, row), new Size(width, height));
            return _sut.IsSliceValid(slice);
        }

        [Theory]
        [InlineData(0, 0, 2, 2)]
        [InlineData(0, 0, 2, 3)]
        [InlineData(0, 0, 3, 2)]
        [InlineData(0, 1, 1, 2)]
        [InlineData(0, 1, 2, 2)]
        [InlineData(0, 1, 2, 3)]
        [InlineData(0, 3, 2, 3)]
        [InlineData(1, 1, 1, 2)]
        [InlineData(1, 0, 2, 1)]
        [InlineData(1, 0, 3, 1)]
        public void WhenCheckingAValidSlice_ShouldReturnTrue(int row, int col, int width, int height) =>
            CheckSlice(row, col, width, height).Should().BeTrue();

        [Fact]
        public void ValidatingASliceBelowMinSize_ShouldReturnFalse() =>
            CheckSlice(0, 0, 1, 1).Should().BeFalse();

        [Fact]
        public void ValidatingASliceAboveMaxSize_ShouldReturnFalse() =>
            CheckSlice(0, 0, 3, 3).Should().BeFalse();

        [Theory]
        [InlineData(-1, 0, 2, 1)]
        [InlineData(0, -1, 2, 1)]
        [InlineData(1, 1, 1, 8)]
        [InlineData(1, 1, 12, 2)]
        [InlineData(2, 1, 1, 2)]
        [InlineData(-10, -10, 120, 200)]
        public void ValidatingSliceOutsideOfPizza_ShouldReturnFalse(int row, int col, int width, int height) =>
            CheckSlice(row, col, width, height).Should().BeFalse();

        [Theory]
        [InlineData(0, 0, 2, 1)]
        [InlineData(0, 0, 1, 3)]
        [InlineData(2, 1, 2, 1)]
        public void WhenCheckingAnInvalidSlice_ShouldReturnFalse(int row, int col, int width, int height) =>
            CheckSlice(row, col, width, height).Should().BeFalse();


    }
}
