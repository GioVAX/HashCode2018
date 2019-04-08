using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_UnitTests_Base
    {
        protected readonly DancingLinksPlatform<int> _sut;
        protected readonly List<TestOption<int>> _options;

        public DLP_UnitTests_Base()
        {
            _sut = new DancingLinksPlatform<int>();
            _options = new List<TestOption<int>>
            {
                new TestOption<int>(new[] { 1, 2, 3 }),
                new TestOption<int>(new[] { 1, 4, 5 }),
                new TestOption<int>(new[] { 6, 7, 8 })
            };

            _options.ForEach(_sut.AddOption);
        }

        [Fact]
        public void Sut_ShouldBeCorrect()
        {
            _sut.Options.Should()
                .BeEquivalentTo(_options);

            _sut.Items.Should()
                .BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8 );

            _sut.ItemHeaders
                .Select(hdr => hdr.Options.Count)
                .Should().ContainInOrder(2, 1, 1, 1, 1, 1, 1, 1);
        }
    }
}