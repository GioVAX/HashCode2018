using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_OverlappingOptions_UnitTests : DLP_UnitTests_Base
    {
        [Fact]
        public void CoverOptionWithOverlaps_ShouldKeep1Option()
        {
            _sut.Cover(_options[0]);

            _sut.Options.Should()
                .BeEquivalentTo(_options[2]);
        }

        [Fact]
        public void CoverOptionWithOverlaps_ShouldKeep5Items()
        {
            _sut.Cover(_options[0]);

            _sut.Items.Should()
                .BeEquivalentTo(4, 5, 6, 7, 8);
        }

        [Fact]
        public void CoverOptionWithOverlaps_ShouldHaveCorrectOptionsForEachItem()
        {
            _sut.Cover(_options[0]);

            _sut.ItemHeaders
                .Select(ih => Tuple.Create(ih.Item, ih.Options.Count))
               .Should()
               .BeEquivalentTo(Tuple.Create(4, 0),
                    Tuple.Create(5, 0), 
                    Tuple.Create(6, 1), 
                    Tuple.Create(7, 1), 
                    Tuple.Create(8, 1));
        }

        [Fact]
        public void CoverOptionWithOverlaps_ShouldRemoveTheItemsFromTheCoveredOption()
        {
            var result = _sut.Cover(_options[0]);

            result.Items.Should()
                .BeEquivalentTo(1, 2, 3);
        }

        [Fact]
        public void CoverOptionWithOverlaps_ShouldRemoveTheOverlappingOptions()
        {
            var coverResult = _sut.Cover(_options[0]);

            coverResult.Options.Should()
                .BeEquivalentTo(_options[0], _options[1]);
        }



        //[Fact]
        //public void CoverOptionWithOverlaps_ShouldRemoveTheOverlappingOptionsFromTheRemainingItems()
        //{
        //    _sut.Cover(_options[0]);

        //    _sut.
        //}

        //[Fact]
        //public void FindOverlappingOptions_UniqueOption_ShouldReturnOnlyTheOption()
        //{
        //    var options = _sut.FindOverlappingOptions(_options[2])
        //        .ToList();

        //    options
        //        .Should().HaveCount(1);

        //    options[0]
        //        .Should().BeSameAs(_options[2]);
        //}
    }
}