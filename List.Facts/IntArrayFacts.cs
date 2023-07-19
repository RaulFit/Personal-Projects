namespace GenericList.Facts
{
    public class IntArrayFacts
    {
        [Fact]
        public void MethodCountReturnsZeroForAnEmptyArray()
        {
            IntArray arr = new IntArray();
            Assert.Equal(0, arr.Count);
        }

        [Fact]
        public void MethodCountReturnsNumberOfElements()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Equal(3, arr.Count);
        }

        [Fact]
        public void MethodElementReturnsTheElementFromSpecifiedIndex()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Equal(2, arr[1]);
        }

        [Fact]
        public void MethodElementWorksWhenArrayDoesNotContainIndex()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Throws<IndexOutOfRangeException>(() => arr[6]);
        }

        [Fact]
        public void MethodSetElementModifiesElementAtSpecifiedIndex()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr[0] = 5;
            Assert.Equal(5, arr[0]);
        }

        [Fact]
        public void MethodSetElementWorksWhenArrayDoesNotContainIndex()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Throws<IndexOutOfRangeException>(() => arr[6] = 5);
        }

        [Fact]
        public void MethodContainsReturnsTrueWhenElementIsInArray()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.True(arr.Contains(3));
        }

        [Fact]
        public void MethodContainsReturnsFalseWhenElementIsNotInArray()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.False(arr.Contains(5));
        }

        [Fact]
        public void MethodIndexOfReturnsNegativeOneWhenElementIsNotInArray()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Equal(-1, arr.IndexOf(5));
        }

        [Fact]
        public void MethodIndexOfReturnsIndexOfElement()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Equal(2, arr.IndexOf(3));
        }

        [Fact]
        public void MethodInsertModifiesArraySize()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(0, 3);
            Assert.Equal(4, arr.Count);
        }

        [Fact]
        public void MethodInsertModifiesElementAtSpecifiedIndex()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(0, 9);
            Assert.Equal(9, arr[0]);
        }

        [Fact]
        public void MethodInsertShiftsElementsFromSpecidiedIndexToTheRightWithOnePosition()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(0, 9);
            Assert.Equal(1, arr[1]);
            Assert.Equal(2, arr[2]);
            Assert.Equal(3, arr[3]);
        }

        [Fact]
        public void MethodClearEmptiesTheArray()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Clear();
            Assert.Equal(0, arr.Count);
        }

        [Fact]
        public void MethodRemoveModifiesArraySize()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(1);
            arr.Remove(1);
            Assert.Equal(3, arr.Count);
        }

        [Fact]
        public void MethodRemoveDeletesFirstAparitionOfElement()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(1);
            arr.Remove(1);
            Assert.Equal(2, arr[0]);
            Assert.Equal(3, arr[1]);
            Assert.Equal(1, arr[2]);
        }

        [Fact]
        public void MethodRemoveAtDoesNotModifiyArrayIfSpecifiedIndexDoesNotExist()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(1);
            arr.RemoveAt(6);
            Assert.Equal(4, arr.Count);
        }

        [Fact]
        public void MethodRemoveRemovesElementAtSpecifiedIndex()
        {
            IntArray arr = new IntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(1);
            arr.RemoveAt(1);
            Assert.Equal(1, arr[0]);
            Assert.Equal(3, arr[1]);
            Assert.Equal(1, arr[2]);
        }
    }
}