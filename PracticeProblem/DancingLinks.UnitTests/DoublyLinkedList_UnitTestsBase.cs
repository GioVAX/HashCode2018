using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;

namespace DancingLinks.UnitTests
{
    public class DoublyLinkedList_UnitTestsBase
    {
        protected DoublyLinkedList<int> Sut { get; }

        protected DoublyLinkedList_UnitTestsBase(DoublyLinkedList<int> sut)
        {
            Sut = sut;
        }

        #region Protected methods
        private List<int> Init(int n, Action<int> action)
        {
            var values = new Fixture().CreateMany<int>(n)
                .ToList();

            values.ForEach(action);

            return values;
        }

        protected List<int> InitWithNValues(int n) => Init(n, val => Sut.AddValue(val));

        protected List<int> InitWithNNodes(int n) => Init(n, val => Sut.AppendNode(new DoublyLinkedListNode<int>(val)));
        #endregion

        protected void CheckOneNodeList(int value)
        {
            Sut.First.Previous
                .Should().NotBeNull();

            Sut.First.Next
                .Should().NotBeNull();

            Sut.Last.Previous
                .Should().NotBeNull();

            Sut.Last.Next
                .Should().NotBeNull();

            Sut.First.Value
                .Should().Be(value);
        }

        protected void CheckTwoNodesList(int val1, int val2)
        {
            Sut.First.Previous
                .Should().BeSameAs(Sut.Last);

            Sut.First.Next
                .Should().BeSameAs(Sut.Last);

            Sut.Last.Next
                .Should().BeSameAs(Sut.First);

            Sut.Last.Previous
                .Should().BeSameAs(Sut.First);

            Sut.First.Value
                .Should().Be(val1);

            Sut.Last.Value
                .Should().Be(val2);
        }
    }
}