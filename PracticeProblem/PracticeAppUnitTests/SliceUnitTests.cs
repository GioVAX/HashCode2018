using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class Slice_2x2_UnitTests
    {
        private readonly Slice _sut;

        public Slice_2x2_UnitTests() =>
            _sut = new Slice(new Point(1, 1), new Size(2, 2));

        [Fact]
        public void ShouldReturnCorrectListOfPoints() =>
            _sut.Points.
                Should().BeEquivalentTo(
                    new List<Point> { new Point(1, 1), new Point(1, 2), new Point(2, 1), new Point(2, 2) });

        [Fact]
        public void ShouldReturnCorrectMapToArrayIndexes() =>
            _sut.MapToArray(5).
                Should().BeEquivalentTo(
                    new List<int> { 6, 7, 11, 12 });

        [Fact]
        public void ShouldComputeCorrectEdges()
        {
            _sut.TopLeft
                .Should().Be(new Point(1, 1));
            _sut.BottomRight
                .Should().Be(new Point(2, 2));
        }
    }

    public class Slice_3x2_UnitTests
    {
        private readonly Slice _sut;

        public Slice_3x2_UnitTests() =>
            _sut = new Slice(new Point(1, 1), new Size(3, 2));

        [Fact]
        public void WhenCreatingA3x2Slice_ShouldReturnCorrectListOfPoints() =>
            _sut.Points
                .Should().BeEquivalentTo(
                    new List<Point>
                    {
                        new Point(1, 1), new Point(1, 2), new Point(2, 1), new Point(2, 2), new Point(3, 1),
                        new Point(3, 2)
                    });

        [Fact]
        public void ShouldReturnCorrectMapToArrayIndexes() =>
            _sut.MapToArray(6).
                Should().BeEquivalentTo(
                    new List<int> { 7, 8, 9, 13, 14, 15 });

        
        [Fact]
        public void ShouldComputeCorrectEdges()
        {
            _sut.TopLeft
                .Should().Be(new Point(1, 1));
            _sut.BottomRight
                .Should().Be(new Point(3, 2));
        }

    }

    public class Slice_2x3_UnitTests
    {
        private readonly Slice _sut;

        public Slice_2x3_UnitTests() =>
            _sut = new Slice(new Point(1, 1), new Size(2, 3));

        [Fact]
        public void WhenCreatingA2x3Slice_ShouldReturnCorrectListOfPoints() =>
            _sut.Points
                .Should().BeEquivalentTo(
                    new List<Point>
                    {
                        new Point(1, 1), new Point(1, 2), new Point(2, 1), new Point(2, 2), new Point(1, 3),
                        new Point(2, 3)
                    });
        
        [Fact]
        public void ShouldReturnCorrectMapToArrayIndexes() =>
            _sut.MapToArray(7).
                Should().BeEquivalentTo(
                    new List<int> { 8, 9, 15, 16, 22,23 });
        
        [Fact]
        public void ShouldComputeCorrectEdges()
        {
            _sut.TopLeft
                .Should().Be(new Point(1, 1));
            _sut.BottomRight
                .Should().Be(new Point(2, 3));
        }

    }
}
