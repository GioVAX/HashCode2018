using AutoFixture;
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
        public void AddingOneValue_ShouldHaveSameFirstAndLastNode()
        {
            Sut.AddValue(Fixture.Create<int>());

            Sut.First
                .Should().BeSameAs(Sut.Last);
        }

        [Fact]
        public void AppendOneNode_ShouldHaveSameFirstAndLastNode()
        {
            var value = Fixture.Create<int>();
            var node = new DoublyLinkedListNode<int>(value);

            Sut.AppendNode(node);

            Sut.First
                .Should().BeSameAs(Sut.Last);
        }

        [Fact]
        public void AddingOneValue_ShouldHaveFirstAndLastPointToEachOther()
        {
            var value = Fixture.Create<int>();
            Sut.AddValue(value);

            CheckOneNodeList(value);
        }

        [Fact]
        public void AppendOneNode_ShouldHaveFirstAndLastPointToEachOther()
        {
            var value = Fixture.Create<int>();
            var node = new DoublyLinkedListNode<int>(value);

            Sut.AppendNode(node);

            CheckOneNodeList(value);
        }

    }
}
