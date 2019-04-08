using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_KnuthPaper_UnitTests
    {
        private readonly DancingLinksPlatform<char> _sut;
        private readonly List<TestOption<char>> _options;

        public DLP_KnuthPaper_UnitTests()
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

        private void VerifyOptionCounts(params Tuple<char, int>[] expected) =>
            _sut.ItemHeaders
                .Select(hdr => Tuple.Create(hdr.Item, hdr.Options.Count))
                .Should().BeEquivalentTo(expected);

        [Fact]
        public void SutInitialStatus_ShouldBeCorrect()
        {
            _sut.Options.Should()
                .BeEquivalentTo(_options);

            VerifyItems('C', 'E', 'F', 'A', 'D', 'G', 'B');
            VerifyOptionCounts(Tuple.Create('C', 2),
                Tuple.Create('E', 2),
                Tuple.Create('F', 2),
                Tuple.Create('A', 2),
                Tuple.Create('D', 3),
                Tuple.Create('G', 3),
                Tuple.Create('B', 2));
        }

        [Fact]
        public void CoverADG_ShouldReturnCorrectResult()
        {
            var result = _sut.Cover(_options[1]);

            result.Items.Should()
                .BeEquivalentTo('A', 'D', 'G');

            result.Options.Should()
                .BeEquivalentTo(_options[1], _options[3], _options[4], _options[5]);
        }

        [Fact]
        public void CoverADG_ShouldResultInCorrectConfiguration()
        {
            _sut.Cover(_options[1]);

            SutStep1_ShouldHaveCorrectConfiguration();
        }

        private void SutStep1_ShouldHaveCorrectConfiguration()
        {
            _sut.Options.Should()
                .HaveCount(2);

            VerifyItems('C', 'E', 'F', 'B');
            VerifyOptionCounts(Tuple.Create('B', 1),
                Tuple.Create('C', 2),
                Tuple.Create('E', 1),
                Tuple.Create('F', 2));
        }

        [Fact]
        public void UncoverADG_ShouldResetSutToInitialConfiguration()
        {
            var result = _sut.Cover(_options[1]);
            _sut.Uncover(result);

            SutInitialStatus_ShouldBeCorrect();
        }

        [Fact]
        public void CoverADG_BCF_ShouldReturnCorrectResult()
        {
            _sut.Cover(_options[1]);
            var result = _sut.Cover(_options[2]);

            result.Items.Should()
                .BeEquivalentTo('B', 'C', 'F');

            result.Options.Should()
                .BeEquivalentTo(_options[0], _options[2]);
        }

        [Fact]
        public void CoverADG_BCF_ShouldResultInCorrectConfiguration()
        {
            _sut.Cover(_options[1]);
            _sut.Cover(_options[2]);

            _sut.Options.Should().BeEmpty();

            VerifyItems('E');
            VerifyOptionCounts(Tuple.Create('E', 0));
        }

        [Fact]
        public void Uncover_BCF_ShouldResetSutToPreviousStep()
        {
            _sut.Cover(_options[1]);
            var result2 = _sut.Cover(_options[2]);

            _sut.Uncover(result2);

            SutStep1_ShouldHaveCorrectConfiguration();
        }


        [Fact]
        public void UncoverADG_BCF_ShouldResultSutToInitialConfiguration()
        {
            var result1 = _sut.Cover(_options[1]);
            var result2 = _sut.Cover(_options[2]);

            _sut.Uncover(result2);
            _sut.Uncover(result1);

            SutInitialStatus_ShouldBeCorrect();
        }

        [Fact]
        public void FullSetCover_ShouldResultInCorrectConfiguration()
        {
            _sut.Cover(_options[3]);
            _sut.Cover(_options[4]);
            _sut.Cover(_options[0]);

            _sut.Options.Should().BeEmpty();
            _sut.Items.Should().BeEmpty();
        }
    }
}
