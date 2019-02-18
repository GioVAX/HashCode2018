using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DancingLinksPlatformUnitTests
    {
        private readonly DancingLinksPlatform _sut;

        public DancingLinksPlatformUnitTests()
        {
            _sut = new DancingLinksPlatform();
        }

        [Fact]
        public void CreatePlatform()
        {
            _sut.Should()
                .NotBeNull();
        }
    }
}
