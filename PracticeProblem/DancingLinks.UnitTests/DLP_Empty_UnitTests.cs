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
        public void EmptyPlatform_ShouldHaveNoOptions() => _sut.Options.Should().BeEmpty();

        [Fact]
        public void EmptyPlatform_ShouldHaveNoItems() => _sut.Items.Should().BeEmpty();

        [Theory, AutoData]
        public void WhenOneItemIsAdded_ShouldHaveCorrectItems(int item)
        {
            _sut.AddItem(item);

            _sut.Items.Should()
                .BeEquivalentTo(item);
        }

        [Theory, AutoData]
        public void WhenMultipleItemsAreAdded_ShouldHaveCorrectItems(List<int> items)
        {
            items.ForEach(_sut.AddItem);

            _sut.Items.Should()
                .HaveCount(items.Count);
        }

        [Theory, AutoData]
        public void WhenOneOptionIsAdded_ShouldHaveCorrectOption(TestOption<int> option)
        {
            _sut.AddOption(option);

            _sut.Options.Should()
                .BeEquivalentTo(option);
        }

        [Theory, AutoData]
        public void WhenOneOptionIsAdded_ShouldBeAddedToRelevantItems(TestOption<int> option)
        {
            _sut.AddOption(option);

            _sut.Items
                .Where(item => option.Items.Contains(item))
                .Should().HaveCount(option.Items.Count());
        }


        [Theory, AutoData]
        public void WhenTwoOptionsAreAdded_ShouldHaveTwoOptionsInTheRightOrder(List<TestOption<int>> options)
        {
            options.ForEach(_sut.AddOption);

            _sut.Options.Should()
                .BeEquivalentTo(options);
        }

        [Theory, AutoData]
        public void WhenTwoOptionsAreAdded_ShouldHaveAllTheItemsOfAllTheOptionsWithNoDuplicates(List<TestOption<int>> options)
        {
            options.ForEach(_sut.AddOption);

            var expectedItems = options.SelectMany(option => option.Items).Distinct().ToList();

            _sut.Items.Should()
                .HaveCount(expectedItems.Count);
        }

        [Fact]
        public void WhenTwoOptionsWithOverlappingItemsAreAdded_ShouldHaveAllTheItemsOfAllTheOptionsWithNoDuplicates()
        {
            _sut.AddOption(new TestOption<int>(new[] { 1, 2, 3 }));
            _sut.AddOption(new TestOption<int>(new[] { 1, 4, 5 }));

            _sut.Items
                .Should().BeEquivalentTo(1, 2, 3, 4, 5);
        }

        [Fact]
        public void WhenExistingItemsAndOneOptionIsAdded_ShouldHaveItemsFromExistingAndOptionsWithNoDuplicates()
        {
            _sut.AddItem(1);
            _sut.AddItem(5);
            _sut.AddOption(new TestOption<int>(new[] { 1, 4, 5 }));

            _sut.Items
                .Should().BeEquivalentTo(1, 4, 5);
        }
    }
}
