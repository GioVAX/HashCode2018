using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_UnitTests
    {
        private readonly DancingLinksPlatform<int> _sut;
        private List<TestOption> _options;

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
                .HaveCount(_options.Count).And
                .ContainInOrder(_options);

            _sut.ItemsCount.Should()
                .Be(8);
        }

        [Fact(Skip = "I am not sure this functionality is necessary")]
        public void CoverUniqueOption_ShouldLeave2Options()
        {
            _sut.Cover(_options[2]);

            _sut.Options.Should()
                .HaveCount(2).And
                .NotContain(_options[2]);
        }

        [Fact]
        public void CoverUniqueOption_ShouldLeave5Items()
        {
            _sut.Cover(_options[2]);

            _sut.ItemsCount.Should()
                .Be(5);
        }

        [Fact]
        public void CoverUniqueOption_ShouldReturnRemovedItems()
        {
            var result = _sut.Cover(_options[2]);

            result.Stack
                .Should().HaveCount(_options[2].Items.Count());
        }

        [Fact]
        public void CoverUniqueOption_ShouldReturnCoveredOption()
        {
            var covered = _sut.Cover(_options[2]);

            covered.Option
                .Should().Be(_options[2]);
        }
    }
}
