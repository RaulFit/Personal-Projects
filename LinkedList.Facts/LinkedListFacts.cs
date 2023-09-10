using System.Formats.Asn1;

namespace MyLinkedList.Facts
{
    public class LinkedListFacts
    {
        [Fact]
        public void AddFirst_ShouldAddNodeAtTheBeginning()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> firstNode = new Node<int>(1);
            Node<int> secondNode = new Node<int>(2);
            list.AddFirst(secondNode);
            list.AddFirst(firstNode);
            Assert.Equal(firstNode, list.First);
        }

        [Fact]
        public void Add_ShouldAddNodeAtTheEnd()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> nodeToAdd = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(nodeToAdd);
            Assert.Equal(nodeToAdd, list.First.Prev);
        }

        [Fact]
        public void InsertAfter_ShouldInsertNodeAfterTheSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            Node<int> nodeToInsert = new Node<int>(5);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(specifiedNode);
            list.InsertAfter(specifiedNode, nodeToInsert);
            Assert.Equal(nodeToInsert, list.First.Prev);
        }

        [Fact]
        public void InsertAfterThrowsExceptionIfInsertAfterNodeIsNull()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> nodeToInsert = new Node<int>(5);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            Assert.Throws<ArgumentNullException>(() => list.InsertAfter(null, nodeToInsert));
        }

        [Fact]
        public void Remove_ShouldReturnTrueWhenSpecifiedNodeIsInTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(specifiedNode);
            Assert.True(list.Remove(specifiedNode));
        }

        [Fact]
        public void Remove_ShouldReturnFalseWhenSpecifiedNodeIsNotInTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            Assert.False(list.Remove(specifiedNode));
        }

        [Fact]
        public void Remove_ShouldReturnFalseWhenListIsEmpty()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(1);
            Assert.False(list.Remove(specifiedNode));
        }

        [Fact]
        public void Find_ShouldReturnSpecifiedNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(specifiedNode);
            Assert.Equal(specifiedNode, list.Find(4));
        }

        [Fact]
        public void Find_ShouldReturnNullWhenSpecifiedNodeIsNotInTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            Assert.Null(list.Find(4));
        }

        [Fact]
        public void Find_ShouldReturnNullWhenListIsEmpty()
        {
            LinkedList<int> list = new LinkedList<int>();
            Assert.Null(list.Find(1));
        }

        [Fact]
        public void Clear_ShouldEmptyTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(new Node<int>(4));
            list.Clear();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void ContainsReturnsTrueIfSpecifiedNodeIsInTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(specifiedNode);
            Assert.Contains(specifiedNode, list);
        }

        [Fact]
        public void ContainsReturnsFalseIfSpecifiedNodeIsNotInTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            Assert.DoesNotContain(specifiedNode, list);
        }

        [Fact]
        public void ContainsReturnsFalseIfListIsEmpty()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            Assert.DoesNotContain(specifiedNode, list);
        }
    }
}