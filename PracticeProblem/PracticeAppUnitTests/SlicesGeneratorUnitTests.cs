using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class SlicesGeneratorUnitTests
    {
        private readonly SlicesGenerator _sut;
        private readonly SliceSizes _sizes;

        public SlicesGeneratorUnitTests()
        {
            _sut = new SlicesGenerator();
            _sizes = new SliceSizes();
        }

        [Fact]
        public void ShouldGenerateAllPossibleSlices()
        {
            var actual = _sut.GenerateAllSlices(2, 2, _sizes.Generate(2, 3));

            actual.Should()
                .NotBeNull()
                .And.AllBeAssignableTo<Slice>()
                .And.BeEquivalentTo(
                    new List<Slice> {
                        new Slice(new Point(0, 0), new Size( 1,2) ),
                        new Slice(new Point(0, 0), new Size( 1,3) ),
                        new Slice(new Point(0, 0), new Size( 2,1) ),
                        new Slice(new Point(0, 0), new Size( 3,1) ),

                        new Slice(new Point(0, 1), new Size( 1,2) ),
                        new Slice(new Point(0, 1), new Size( 1,3) ),
                        new Slice(new Point(0, 1), new Size( 2,1) ),
                        new Slice(new Point(0, 1), new Size( 3,1) ),

                        new Slice(new Point(1, 0), new Size( 1,2) ),
                        new Slice(new Point(1, 0), new Size( 1,3) ),
                        new Slice(new Point(1, 0), new Size( 2,1) ),
                        new Slice(new Point(1, 0), new Size( 3,1) ),

                        new Slice(new Point(1, 1), new Size( 1,2) ),
                        new Slice(new Point(1, 1), new Size( 1,3) ),
                        new Slice(new Point(1, 1), new Size( 2,1) ),
                        new Slice(new Point(1, 1), new Size( 3,1) )
                    }
                );
        }
    }
}
