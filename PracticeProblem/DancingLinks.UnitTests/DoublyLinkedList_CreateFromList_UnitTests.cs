using System.Linq;
using FluentAssertions;
using Xunit;

namespace DancingLinks.UnitTests
{
    public class DoublyLinkedList_CreateFromList_UnitTests : DoublyLinkedList_UnitTestsBase
    {
        public DoublyLinkedList_CreateFromList_UnitTests() 
            : base(new DoublyLinkedList<int>(Enumerable.Range(1, 2)))
        { }

        [Fact]
        public void CreateAListWithTwoValues_ShouldHaveDifferentFirstAndLastNode() =>
            Sut.First
                .Should().NotBeSameAs(Sut.Last);

        [Fact]
        public void CreateAListWithTwoValues_ShouldHaveProperSequenceOfNodes()
        {
            CheckTwoNodesList(1, 2);
        }

    }
}
