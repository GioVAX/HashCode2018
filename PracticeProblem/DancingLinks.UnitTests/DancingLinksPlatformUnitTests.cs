using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DancingLinksPlatformUnitTests
    {
        private readonly DancingLinksPlatform<int> _sut;

        public DancingLinksPlatformUnitTests() => _sut = new DancingLinksPlatform<int>();

        [Fact]
        public void EmptyPlatform_ShouldHaveEmptyOptions() => _sut.Options.Should().BeEmpty();

        [Fact]
        public void EmptyPlatform_ShouldHaveEmptyItems() => _sut.Items.Should().BeEmpty();

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

            _sut.Items
                .Should().HaveCount(expectedItems.Count)
                .And.ContainInOrder(expectedItems);
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
        public void WhenTwoOptionsAreAdded_ShouldHaveAllTheItemsOfAllTheOptions(List<TestOption> options)
        {
            options.ForEach(_sut.AddOption);

            var expectedItems = options.SelectMany(option => option.Items).Distinct().ToList();

            _sut.Items
                .Should().HaveCount(expectedItems.Count)
                .And.BeEquivalentTo(expectedItems);
        }

        [Theory, AutoData]
        public void WhenTwoOptionsAreAdded_ShouldNotContainDuplicatedItems(List<TestOption> options)
        {
            options.ForEach(_sut.AddOption);

            _sut.AddOption(options[0]);

            _sut.Items
                .Should().OnlyHaveUniqueItems();
        }
    }
}
