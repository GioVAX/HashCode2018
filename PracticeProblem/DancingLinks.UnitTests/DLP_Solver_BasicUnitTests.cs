using System.Collections.Generic;
using System.Linq;
using DancingLinks;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DLP_Solver_BasicUnitTests
    {
        private readonly DancingLinksSolver<char> _sut;
        private readonly List<TestOption<char>> _options;

        public DLP_Solver_BasicUnitTests()
        {
            _sut = new DancingLinksSolver<char>();

            _options = new List<TestOption<char>>
            {
                new TestOption<char>(new []  {'C', 'E', 'F'}),
                new TestOption<char>(new []  {'A', 'D', 'G'}),
                new TestOption<char>(new []  {'B', 'C', 'F'}),
                new TestOption<char>(new []  {'A', 'D'} ),
                new TestOption<char>(new []  {'B', 'G'} ),
                new TestOption<char>(new []  {'D', 'E', 'G'}),
            };
        }

        [Fact]
        public void Sut_ShouldBeCorrect() => _sut.Should().NotBeNull();

        [Fact]
        public void WhenProblemHasSolution_ShouldFindCorrectSolution()
        {
            _options.ForEach(_sut.AddOption);

            var result = _sut.Solve();

            result.Items.Should()
                .BeEquivalentTo(_options[3], _options[4], _options[0]);
        }

        [Fact]
        public void WhenProblemHasNoSolution_ShouldFindNullSolution()
        {
            foreach (var option in _options.Skip(1))
                _sut.AddOption(option);

            var result = _sut.Solve();

            result.Items.Should()
                .HaveCount(0);
        }

        [Fact]
        public void WhenAddingItemsToSolvableProblem_ShouldFindNullSolution()
        {
            _options.ForEach(_sut.AddOption);
            _sut.AddItem('Z');

            var result = _sut.Solve();

            result.Items.Should()
                .HaveCount(0);
        }
    }
}
