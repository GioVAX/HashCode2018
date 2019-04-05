using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_Classic_UnitTests
    {
        private readonly DancingLinksPlatform<char> _sut;
        private List<TestOption<char>> _options;

        public DLP_Classic_UnitTests()
        {
            _sut = new DancingLinksPlatform<char>();

            _options = new List<TestOption<char>>
            {
                new TestOption<char>(new []  {'C', 'E', 'F'}),
                new TestOption<char>(new []  {'A', 'D', 'G'}),
                new TestOption<char>(new []  {'B', 'C', 'F'}),
                new TestOption<char>(new []  {'A', 'D'} ),
                new TestOption<char>(new []  {'B', 'G'} ),
                new TestOption<char>(new []  {'D', 'E', 'G'}),
            };

            _options.ForEach(_sut.AddOption);
        }

        private void VerifyItems(params char[] expected) =>
            _sut.Items.Should()
                .ContainInOrder(expected);

        private void VerifyOptionCounts(params int[] expected) =>
            _sut.ItemHeaders
                .Select(hdr => hdr.Options.Count)
                .Should().ContainInOrder(expected);

        [Fact]
        public void Sut_ShouldBeCorrect()
        {
            _sut.Options.Should()
                .BeEquivalentTo(_options);

            VerifyItems('C', 'E', 'F', 'A', 'D', 'G', 'B');
            VerifyOptionCounts(2, 2, 2, 2, 3, 3, 2);
        }

        //#region Cover Tests
        //[Fact]
        //public void CoverUniqueOption_ShouldKeep2Options()
        //{
        //    _sut.Cover(_options[2]);

        //    _sut.Options.Should()
        //        .HaveCount(2).And
        //        .NotContain(_options[2]);
        //}

        //[Fact]
        //public void CoverUniqueOption_ShouldKeep5Items()
        //{
        //    _sut.Cover(_options[2]);

        //    _sut.Items
        //        .Should().BeEquivalentTo(1, 2, 3, 4, 5);
        //}

        //[Fact]
        //public void CoverUniqueOption_ShouldRemoveTheItem()
        //{
        //    var result = _sut.Cover(_options[2]);

        //    result.Items
        //        .Should().BeEquivalentTo(_options[2].Items);
        //}

        //[Fact]
        //public void CoverUniqueOption_ShouldRemoveTheCoveredOption()
        //{
        //    var covered = _sut.Cover(_options[2]);

        //    covered.Options
        //        .Should().BeEquivalentTo(_options[2]);
        //}

        //[Fact]
        //public void CoverOptionWithOverlaps_ShouldRemoveTheItemsFromTheCoveredOption()
        //{
        //    var result = _sut.Cover(_options[0]);

        //    result.Items.Should()
        //        .BeEquivalentTo(1, 2, 3);
        //}

        //[Fact]
        //public void CoverOptionWithOverlaps_ShouldRemoveTheOverlappingOptions()
        //{
        //    var coverResult = _sut.Cover(_options[0]);

        //    coverResult.Options.Should()
        //        .BeEquivalentTo(_options[0], _options[1]);
        //} 
        //#endregion

        //#region Uncover Tests

        //[Theory]
        //[InlineData(0)]
        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(4)]
        //[InlineData(5)]
        //[InlineData(8)]
        //[InlineData(3)]
        //[InlineData(11)]
        //public void UncoverUniqueItem_WillRestoreInitialStatus(int idx)
        //{
        //    var coverResult = _sut.Cover(_options[idx % 3]);

        //    _sut.Uncover(coverResult);

        //    Sut_ShouldBeCorrect();
        //}
        //#endregion

        ////[Fact]
        ////public void FindOverlappingOptions_UniqueOption_ShouldReturnOnlyTheOption()
        ////{
        ////    var options = _sut.FindOverlappingOptions(_options[2])
        ////        .ToList();

        ////    options
        ////        .Should().HaveCount(1);

        ////    options[0]
        ////        .Should().BeSameAs(_options[2]);
        ////}
    }
}
