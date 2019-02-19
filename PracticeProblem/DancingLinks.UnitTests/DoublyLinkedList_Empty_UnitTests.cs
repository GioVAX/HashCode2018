using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DoublyLinkedList_Empty_UnitTests : DoublyLinkedList_UnitTestsBase
    {
        public DoublyLinkedList_Empty_UnitTests()
            : base(new DoublyLinkedList<int>())
        { }

        [Fact]
        public void WhenCreated_ShouldHaveFirstNull() => Sut.First.Should().BeNull();

        [Fact]
        public void WhenCreated_ShouldHaveLastNull() => Sut.Last.Should().BeNull();

        [Fact]
        public void WhenCreated_ShouldHaveValuesEmpty() => Sut.Values.Should().NotBeNull().And.BeEmpty();

        [Theory, AutoData]
        public void AddingOneValue_ShouldHaveSameFirstAndLastNode(int aValue)
        {
            Sut.AddValue(aValue);

            Sut.First
                .Should().BeSameAs(Sut.Last);
        }

        [Theory, AutoData]
        public void AppendOneNode_ShouldHaveSameFirstAndLastNode(int aValue)
        {
            var node = new DoublyLinkedListNode<int>(aValue);

            Sut.AppendNode(node);

            Sut.First
                .Should().BeSameAs(Sut.Last);
        }

        [Theory, AutoData]
        public void AddingOneValue_ShouldHaveFirstAndLastPointToEachOther([Frozen]int value)
        {
            Sut.AddValue(value);

            CheckOneNodeList(value);
        }

        [Theory, AutoData]
        public void AppendOneNode_ShouldHaveFirstAndLastPointToEachOther([Frozen]int value)
        {
            var node = new DoublyLinkedListNode<int>(value);

            Sut.AppendNode(node);

            CheckOneNodeList(value);
        }
    }
}
