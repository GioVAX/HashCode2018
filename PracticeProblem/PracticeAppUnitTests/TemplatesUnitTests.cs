using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class TemplatesUnitTests
    {
        private SliceSizes _sut;

        public TemplatesUnitTests()
        {
            _sut = new SliceSizes();
        }

        [Fact]
        public void WhenAskedTemplates_ShouldReturnListOfTemplates()
        {
            _sut.Generate(3, 4).Should()
                .NotBeNull()
                .And.OnlyHaveUniqueItems()
                .And.AllBeOfType(typeof(Size));
        }

        [Fact]
        public void WhenAskedTemplatesBetween2And4_ShouldReturnCorrectListOfTemplates()
        {
            _sut.Generate(2, 4).Should()
                .BeEquivalentTo(
                    new List<Size>
                    {
                        new Size(1, 2),
                        new Size(2, 1),
                        new Size(1, 3),
                        new Size(3, 1),
                        new Size(1, 4),
                        new Size(2, 2),
                        new Size(4, 1)
                    }
                );
        }
    }
}
