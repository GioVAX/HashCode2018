using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DoublyLinkedList_UnitTests : DoublyLinkedList_UnitTestsBase
    {
        public DoublyLinkedList_UnitTests()
            : base(new DoublyLinkedList<int>())
        { }
        
        [Theory, AutoData]
        public void AddingOneValue_ShouldReturnTheNewlyInsertedNode(int aValue)
        {
            var node = Sut.AddValue(aValue);

            node.Should().Be(Sut.First);
            node.Should().Be(Sut.Last);
            node.Next.Should().Be(node);
            node.Previous.Should().Be(node);
        }

        [Fact]
        public void AddingTwoValues_ShouldHaveDifferentFirstAndLastNode()
        {
            InitWithNValues(2);

            Sut.First
                .Should().NotBeSameAs(Sut.Last);
        }

        [Fact]
        public void AddingTwoValues_ShouldHaveProperSequenceOfNodes()
        {
            var values = InitWithNValues(2);

            CheckTwoNodesList(values[0], values[1]);
        }

        [Fact]
        public void AppendingTwoNodes_ShouldHaveProperSequenceOfNodes()
        {
            var values = InitWithNNodes(2);
            CheckTwoNodesList(values[0], values[1]);
        }

        [Theory, AutoData]
        public void InsertNodeWithANullInNextOrPrev_ShouldAppendToEndOfList([Frozen]int newValue)
        {
            var values = InitWithNNodes(2);

            var node = new DoublyLinkedListNode<int>(newValue);

            Sut.InsertNode(node);

            Sut.First.Value
                .Should().Be(values[0]);

            Sut.Last.Value
                .Should().Be(newValue);
        }

        [Fact]
        public void InsertARemovedNode_ShouldRestoreTheListOrder()
        {
            var values = InitWithNNodes(3);

            var node = Sut.First.Next;
            Sut.RemoveNode(node);

            Sut.InsertNode(node);

            Sut.First.Next
                .Should().BeSameAs(node);
            Sut.Last.Previous
                .Should().BeSameAs(node);

            Sut.First.Value
                .Should().Be(values[0]);
            Sut.First.Next.Value
                .Should().Be(values[1]);
            Sut.Last.Value
                .Should().Be(values[2]);
        }

        [Theory, AutoData]
        public void RemoveNode_WhenLastNode_ShouldNullFirstAndLast(int aValue)
        {
            Sut.AddValue(aValue);

            Sut.RemoveNode(Sut.First);

            Sut.First
                .Should().BeNull();
            Sut.Last
                .Should().BeNull();
        }

        [Fact]
        public void RemoveNode_ShouldConnectPrevAndNextNode()
        {
            var values = InitWithNValues(3);

            Sut.RemoveNode(Sut.First.Next);

            Sut.First.Next
                .Should().BeSameAs(Sut.Last);
            Sut.Last.Previous
                .Should().BeSameAs(Sut.First);

            Sut.First.Value
                .Should().Be(values[0]);
            Sut.Last.Value
                .Should().Be(values[2]);
        }

        
        [Fact]
        public void RemoveNode_ShouldReturnTheRemovedNode()
        {
            InitWithNValues(3);

            var node = Sut.First.Next;

            Sut.RemoveNode(node).Should()
                .BeSameAs(node);
        }

        [Fact]
        public void RemoveNode_ShouldNotChangeTheRemovedNode()
        {
            InitWithNValues(3);

            var node = Sut.First.Next;
            var prev = node.Previous;
            var next = node.Next;

            Sut.RemoveNode(Sut.First.Next);

            node.Next
                .Should().BeSameAs(next);
            node.Previous
                .Should().BeSameAs(prev);
        }

        [Fact]
        public void EnumeratingDLL_ShouldReturnAllValuesInTheOrderTheyWereAdded()
        {
            var expectedValues = InitWithNValues(15);

            var values = Sut.Values.ToList();

            values
                .Should().ContainInOrder(expectedValues);
        }
    }
}