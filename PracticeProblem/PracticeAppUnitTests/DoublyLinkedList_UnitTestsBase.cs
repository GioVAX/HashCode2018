using AutoFixture;
using FluentAssertions;
using PracticeApp;

namespace PracticeAppUnitTests
{
    public class DoublyLinkedList_UnitTestsBase
    {
        protected DoublyLinkedList<int> Sut { get; }
        protected Fixture Fixture { get; }

        protected DoublyLinkedList_UnitTestsBase(DoublyLinkedList<int> sut)
        {
            Sut = sut;
            Fixture = new Fixture();
        }

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