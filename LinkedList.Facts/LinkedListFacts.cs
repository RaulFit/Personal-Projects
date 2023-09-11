namespace MyLinkedList.Facts
{
    public class LinkedListFacts
    {
        [Fact]
        public void AddLast_ShouldAddNodeAtTheEnd()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.Equal(4, list.GetLast());
        }

        [Fact]
        public void AddFirst_ShouldAddNodeAtTheBeginning()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(4);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);
            Assert.Equal(1, list.GetFirst());
        }

        [Fact]
        public void Remove_ShouldReturnTrueWhenSpecifiedItemIsInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.True(list.Remove(3));
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void Remove_ShouldReturnFalseWhenSpecifiedItemIsNotInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.False(list.Remove(8));
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public void Contains_ShouldReturnTrueWhenSpecifiedItemIsInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.True(list.Contains(2));
        }

        [Fact]
        public void Contains_ShouldReturnFalseWhenSpecifiedItemIsNotInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.False(list.Contains(6));
        }

        [Fact]
        public void IsEmpty_ShouldReturnTrueWhenListIsEmpty()
        {
            LinkedList<int> list = new LinkedList<int>();
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void GetFirst_ShouldReturnFirstElementInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.Equal(1, list.GetFirst());
        }

        [Fact]
        public void GetLast_ShouldReturnLastElementInList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.Equal(4, list.GetLast());
        }

        [Fact]
        public void Get_ShouldReturnCurrentElement()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            Assert.Equal(4, list.Get());
        }

        [Fact]
        public void Set_ShoulSetValueOfCurrentElement()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.Set(5);
            Assert.Equal(5, list.Get());
        }
    }
}