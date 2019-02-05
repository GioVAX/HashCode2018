using System.Collections.Generic;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class TemplatesUnitTests
    {
        private Templates _sut;

        public TemplatesUnitTests()
        {
            _sut = new Templates();
        }

        [Fact]
        public void WhenAskedTemplates_ShouldReturnListOfTemplates()
        {
            _sut.Generate(3, 4).Should()
                .NotBeNull()
                .And.OnlyHaveUniqueItems()
                .And.AllBeOfType(typeof(SliceTemplate));
        }

        [Fact]
        public void WhenAskedTemplatesBetween2And4_ShouldReturnCorrectListOfTemplates()
        {
            _sut.Generate(2, 4).Should()
                .BeEquivalentTo(
                    new List<SliceTemplate>
                    {
                        new SliceTemplate(1, 2),
                        new SliceTemplate(2, 1),
                        new SliceTemplate(1, 3),
                        new SliceTemplate(3, 1),
                        new SliceTemplate(1, 4),
                        new SliceTemplate(2, 2),
                        new SliceTemplate(4, 1)
                    }
                );
        }
    }
}
