using Xunit;

namespace MyLinkedList.Facts
{
    public class LinkedListFacts
    {
        [Fact]
        public void AddFirstWithNode_ShouldAddNodeAtTheBeginning()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(new Node<int>(3));
            list.AddFirst(new Node<int>(2));
            list.AddFirst(new Node<int>(1));
            Assert.Equal(1, list.First.Data);
        }

        [Fact]
        public void AddFirstWithData_ShouldCreateAndAddNodeAtTheBeginning()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);
            Assert.Equal(1, list.First.Data);
        }

        [Fact]
        public void AddLastWithNode_ShouldAddNodeAtTheEnd()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(new Node<int>(1));
            list.AddLast(new Node<int>(2));
            list.AddLast(new Node<int>(3));
            Assert.Equal(3, list.Last.Data);
        }

        [Fact]
        public void AddLastWithData_ShouldCreateAndAddNodeAtTheEnd()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            Assert.Equal(3, list.Last.Data);
        }

        [Fact]
        public void Find_ShouldReturnFirstNodeWithSpecifiedData()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> first = new Node<int>(2);
            Node<int> second = new Node<int>(2);
            list.AddLast(first);
            list.AddLast(second);
            Assert.Equal(first, list.Find(2));
        }

        [Fact]
        public void Find_ShouldReturnNullWhenNodeIsNotInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> first = new Node<int>(2);
            Node<int> second = new Node<int>(2);
            list.AddLast(first);
            list.AddLast(second);
            Assert.Null(list.Find(5));
        }

        [Fact]
        public void FindLast_ShouldReturnLastNodeWithSpecifiedData()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> first = new Node<int>(2);
            Node<int> second = new Node<int>(2);
            list.AddLast(first);
            list.AddLast(second);
            Assert.Equal(second, list.FindLast(2));
        }

        [Fact]
        public void FindLast_ShouldReturnNullWhenNodeIsNotInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> first = new Node<int>(2);
            Node<int> second = new Node<int>(2);
            list.AddLast(first);
            list.AddLast(second);
            Assert.Null(list.FindLast(5));
        }

        [Fact]
        public void AddAfterWithNodes_ShouldAddNodeAfterSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> prevNode = new Node<int>(1);
            Node<int> newNode = new Node<int>(2);
            list.AddLast(prevNode);
            list.AddLast(3);
            list.AddAfter(prevNode, newNode);
            Assert.Equal(newNode, list.Find(2));
        }

        [Fact]
        public void AddAfterWithData_ShouldCreateAndAddNodeAfterSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> prevNode = new Node<int>(1);
            list.AddLast(prevNode);
            list.AddLast(3);
            Node<int> newNode = list.AddAfter(prevNode, 2);
            Assert.Equal(newNode, list.Find(2));
        }

        [Fact]
        public void AddBeforeWithNodes_ShouldAddNodeBeforeSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> nextNode = new Node<int>(3);
            Node<int> newNode = new Node<int>(2);
            list.AddLast(1);
            list.AddLast(nextNode);
            list.AddBefore(nextNode, newNode);
            Assert.Equal(newNode, list.Find(2));
        }

        [Fact]
        public void AddBeforeWithData_ShouldCreateAndAddNodeBeforeSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> nextNode = new Node<int>(3);
            list.AddLast(1);
            list.AddLast(nextNode);
            Node<int> newNode = list.AddBefore(nextNode, 2);
            list.AddBefore(nextNode, newNode);
            Assert.Equal(newNode, list.Find(2));
        }

        [Fact]
        public void Clear_ShouldEmptyTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.Clear();
            Assert.Empty(list);
        }

        [Fact]
        public void Contains_ShoulReturnTrueWhenSpecifiedItemIsInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            Assert.True(list.Contains(2));
        }

        [Fact]
        public void Contains_ShoulReturnFalseWhenSpecifiedItemIsNotInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            Assert.False(list.Contains(5));
        }

        [Fact]
        public void RemoveWithData_ShouldRemoveSpecifiedValue()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(new Node<int>(1));
            list.AddLast(new Node<int>(2));
            list.AddLast(new Node<int>(3));
            Assert.True(list.Remove(2));
            Assert.DoesNotContain(2, list);
        }

        [Fact]
        public void RemoveWithNode_ShouldRemoveSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(2);
            list.AddLast(new Node<int>(1));
            list.AddLast(specifiedNode);
            list.AddLast(new Node<int>(3));
            list.Remove(specifiedNode);
            Assert.DoesNotContain(specifiedNode.Data, list);
        }

        [Fact]
        public void RemoveWithNode_ShouldReturnWhenNodeIsNotInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> nodeToRemove = new Node<int>(5);
            list.AddLast(new Node<int>(1));
            list.AddLast(new Node<int>(2));
            list.AddLast(new Node<int>(3));
            list.Remove(nodeToRemove);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void RemoveFirst_ShouldRemoveFirstNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(new Node<int>(1));
            list.AddLast(new Node<int>(2));
            list.AddLast(new Node<int>(3));
            list.RemoveFirst();
            Assert.DoesNotContain(1, list);
        }

        [Fact]
        public void RemoveFirst_ShouldThrowExceptionWhenListIsEmpty()
        {
            LinkedList<int> list = new LinkedList<int>();
            Assert.Throws<InvalidOperationException>(() => list.RemoveFirst());
        }

        [Fact]
        public void RemoveLast_ShouldRemoveLastNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(new Node<int>(1));
            list.AddLast(new Node<int>(2));
            list.AddLast(new Node<int>(3));
            list.RemoveLast();
            Assert.DoesNotContain(3, list);
        }

        [Fact]
        public void RemoveLast_ShouldThrowExceptionWhenListIsEmpty()
        {
            LinkedList<int> list = new LinkedList<int>();
            Assert.Throws<InvalidOperationException>(() => list.RemoveLast());
        }
    }
}
