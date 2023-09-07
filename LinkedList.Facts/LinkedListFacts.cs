using System.Formats.Asn1;

namespace MyLinkedList.Facts
{
    public class LinkedListFacts
    {
        [Fact]
        public void GetLastNode_ShouldReturnTheLastNode()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> lastNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(lastNode);
            Assert.Equal(lastNode, list.GetLastNode());
        }

        [Fact]
        public void AddFirst_ShouldAddNodeAtTheBeginning()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> nodeToAdd = new Node<int>(1);
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            list.Add(new Node<int>(4));
            list.AddFirst(nodeToAdd);
            Assert.Equal(nodeToAdd, list.First);
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
            Assert.Empty(list);
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
            Assert.True(list.Contains(specifiedNode));
        }

        [Fact]
        public void ContainsReturnsFalseIfSpecifiedNodeIsNotInTheList()
        {
            LinkedList<int> list = new LinkedList<int>();
            Node<int> specifiedNode = new Node<int>(4);
            list.Add(new Node<int>(1));
            list.Add(new Node<int>(2));
            list.Add(new Node<int>(3));
            Assert.False(list.Contains(specifiedNode));
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
            Assert.Equal(nodeToInsert, list.GetLastNode());
        }
    }
}