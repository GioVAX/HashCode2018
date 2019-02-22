using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_Empty_UnitTests
    {
        private readonly DancingLinksPlatform<int> _sut;

        public DLP_Empty_UnitTests() => _sut = new DancingLinksPlatform<int>();

        [Fact]
        public void EmptyPlatform_ShouldHaveEmptyOptions() => _sut.Options.Should().BeEmpty();

        [Fact]
        public void EmptyPlatform_ShouldHaveEmptyItems() => _sut.ItemsCount.Should().Be(0);

        [Theory, AutoData]
        public void WhenOneOptionIsAdded_ShouldHaveCorrectOption(TestOption option)
        {
            _sut.AddOption(option);

            _sut.Options
                .Should().HaveCount(1)
                .And.Subject.First()
                    .Should().Be(option);
        }

        [Theory, AutoData]
        public void WhenOneOptionIsAdded_ShouldHaveCorrectItems(TestOption option)
        {
            _sut.AddOption(option);
            var expectedItems = option.Items.ToList();

            _sut.ItemsCount
                .Should().Be(expectedItems.Count);
        }

        [Theory, AutoData]
        public void WhenTwoOptionsAreAdded_ShouldHaveTwoOptionsInTheRightOrder(List<TestOption> options)
        {
            options.ForEach(_sut.AddOption);

            _sut.Options
                .Should().HaveCount(options.Count)
                .And.ContainInOrder(options);
        }

        [Theory, AutoData]
        public void WhenTwoOptionsAreAdded_ShouldHaveAllTheItemsOfAllTheOptionsWithNoDuplicates(List<TestOption> options)
        {
            options.ForEach(_sut.AddOption);

            var expectedItems = options.SelectMany(option => option.Items).Distinct().ToList();

            _sut.ItemsCount
                .Should().Be(expectedItems.Count);
        }
    }
}
