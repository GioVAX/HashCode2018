using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    public class DoublyLinkedList_UnitTests : DoublyLinkedList_UnitTestsBase
    {
        public DoublyLinkedList_UnitTests()
            : base(new DoublyLinkedList<int>())
        { }

        #region Private methods
        private List<int> Init(int n, Action<int> action)
        {
            var values = Fixture.CreateMany<int>(n)
                .ToList();

            foreach (var v in values)
                action(v);

            return values;
        }

        private List<int> InitWithNValues(int n) => Init(n, val => Sut.AddValue(val));

        private List<int> InitWithNNodes(int n) => Init(n, val => Sut.AppendNode(new DoublyLinkedListNode<int>(val)));
        #endregion

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

        [Fact]
        public void InsertNodeWithANullInNextOrPrev_ShouldAppendToEndOfList()
        {
            var values = InitWithNNodes(2);

            var newValue = Fixture.Create<int>();
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

        [Fact]
        public void RemoveNode_WhenLastNode_ShouldNullFirstAndLast()
        {
            Sut.AddValue(Fixture.Create<int>());

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
    }
}