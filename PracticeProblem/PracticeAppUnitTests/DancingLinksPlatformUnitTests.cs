using FluentAssertions;
using PracticeApp;

namespace PracticeAppUnitTests
{
    public class DancingLinksPlatformUnitTests
    {
        private readonly DancingLinksPlatform _sut;

        //[Fact]
        public void CreatePlatform()
        {
            _sut.Should()
                .NotBeNull();
        }
    }
}
