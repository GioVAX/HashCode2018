using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DancingLinksPlatformUnitTests
    {
        private readonly DancingLinksPlatform<int> _sut;
        private readonly Fixture _fixture;

        public DancingLinksPlatformUnitTests()
        {
            _sut = new DancingLinksPlatform<int>();
            _fixture = new Fixture();
        }

        [Fact]
        public void EmptyPlatform_ShouldHaveEmptyOptions() => _sut.Options.Should().BeEmpty();

        [Fact]
        public void EmptyPlatform_ShouldHaveEmptyItems() => _sut.Items.Should().BeEmpty();

        [Fact]
        public void WhenOneOptionIsAdded_ShouldHaveCorrectOption()
        {
            var option = new TestOption(new[] { 1, 2, 3 });
            _sut.AddOption(option);

            _sut.Options
                .Should().HaveCount(1)
                .And.Subject.First()
                    .Should().Be(option);
        }

        [Fact]
        public void WhenTwoOptionsAreAdded_ShouldHaveTwoOptionsInTheRightOrder()
        {
            var option1 = _fixture.Create<TestOption>();
            var option2 = _fixture.Create<TestOption>();
            _sut.AddOption(option1);
            _sut.AddOption(option2);

            _sut.Options
                .Should().HaveCount(2)
                .And.ContainInOrder(new List<TestOption> { option1, option2 });
        }
    }
}
