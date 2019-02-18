using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DancingLinksPlatformUnitTests
    {
        private readonly DancingLinksPlatform<int> _sut;

        public DancingLinksPlatformUnitTests()
        {
            _sut = new DancingLinksPlatform<int>();
        }

        [Fact]
        public void EmptyPlatform_ShouldHave0OptionsAnd0Items()
        {
            _sut.Options
                .Should().BeEmpty();

            _sut.Options
                .Should().BeEmpty();
        }
    }
}
