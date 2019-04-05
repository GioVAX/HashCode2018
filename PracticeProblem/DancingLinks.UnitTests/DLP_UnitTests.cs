using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_UnitTests
    {
        private readonly DancingLinksPlatform<int> _sut;
        private readonly List<TestOption> _options;

        public DLP_UnitTests()
        {
            _sut = new DancingLinksPlatform<int>();
            _options = new List<TestOption>
            {
                new TestOption(new[] { 1, 2, 3 }),
                new TestOption(new[] { 1, 4, 5 }),
                new TestOption(new[] { 6, 7, 8 })
            };

            _options.ForEach(_sut.AddOption);
        }

        [Fact]
        public void Sut_ShouldBeCorrect()
        {
            _sut.Options.Should()
                .BeEquivalentTo(_options);

            _sut.Items.Should()
                .BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8 );

            _sut.ItemHeaders
                .Select(hdr => hdr.Options.Count)
                .Should().ContainInOrder(2, 1, 1, 1, 1, 1, 1, 1);
        }

        #region Cover Tests
        [Fact]
        public void CoverUniqueOption_ShouldKeep2Options()
        {
            _sut.Cover(_options[2]);

            _sut.Options.Should()
                .HaveCount(2).And
                .NotContain(_options[2]);
        }

        [Fact]
        public void CoverUniqueOption_ShouldKeep5Items()
        {
            _sut.Cover(_options[2]);

            _sut.Items
                .Should().BeEquivalentTo(1, 2, 3, 4, 5);
        }

        [Fact]
        public void CoverUniqueOption_ShouldRemoveTheItem()
        {
            var result = _sut.Cover(_options[2]);

            result.Items
                .Should().BeEquivalentTo(_options[2].Items);
        }

        [Fact]
        public void CoverUniqueOption_ShouldRemoveTheCoveredOption()
        {
            var covered = _sut.Cover(_options[2]);

            covered.Options
                .Should().BeEquivalentTo(_options[2]);
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
        #endregion

        #region Uncover Tests

        [Fact]
        public void UncoverUniqueItem_WillRestoreInitialStatus()
        {
            var coverResult = _sut.Cover(_options[2]);

            _sut.Uncover(coverResult);

            Sut_ShouldBeCorrect();
        }

        #endregion

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
