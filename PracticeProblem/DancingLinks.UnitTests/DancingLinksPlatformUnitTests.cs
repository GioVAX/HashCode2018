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
    }
}
