﻿using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_UniqueOption_UnitTests : DLP_UnitTests_Base
    {
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


        #endregion

        #region Uncover Tests

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(3)]
        [InlineData(11)]
        public void UncoverUniqueItem_WillRestoreInitialStatus(int idx)
        {
            var coverResult = _sut.Cover(_options[idx % 3]);

            _sut.Uncover(coverResult);

            Sut_ShouldBeCorrect();
        }
        #endregion

  
    }
}