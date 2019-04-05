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

            _sut.Items.Should()
                .HaveCount(8);
        }

        [Fact]
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

            _sut.Items
                .Should().BeEquivalentTo(new[] {1, 2, 3, 4, 5});
        }

        //[Fact]
        //public void CoverUniqueOption_ShouldReturnRemovedItems()
        //{
            //var result = _sut.Cover(_options[2]);

            //result.Stack
            //    .Should().HaveCount(_options[2].Items.Count());
        //}

        //[Fact]
        //public void CoverUniqueOption_ShouldReturnCoveredOption()
        //{
        //    var covered = _sut.Cover(_options[2]);

        //    covered.Option
        //        .Should().Be(_options[2]);
        //}

        //[Fact]
        //public void CoverOptionWithOverlaps_ShouldReturnTheItemsFromTheCoveredOption()
        //{
        //    var result = _sut.Cover(_options[0]);

        //    result.Stack
        //        .Should().HaveCount(3);
        //}

        //[Fact]
        //public void CoverOptionWithOverlaps_ShouldReturnTheAdditionalOptionsRemoved()
        //{
        //    var otherOptions = _sut.Cover(_options[0])
        //        .OtherOptions.ToList();

        //    otherOptions
        //        .Should().HaveCount(1);

        //    otherOptions[0]
        //        .Should().BeSameAs(_options[1]);
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
