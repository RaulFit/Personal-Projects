namespace List.Facts
{
    public class SortedIntArrayFacts
    {
        
        [Fact]
        public void ElementsWillBeSortedAfterUsingAddMethod()
        {
            SortedIntArray arr = new SortedIntArray();
            arr.Add(3);
            arr.Add(2);
            arr.Add(1);
            arr.Add(10);
            arr.Add(7);
            Assert.Equal(1, arr[0]);
            Assert.Equal(2, arr[1]);
            Assert.Equal(3, arr[2]);
            Assert.Equal(7, arr[3]);
            Assert.Equal(10, arr[4]);
        }

        [Fact]
        public void ElementsWillNotBeModifiedWhenArrayIsSorted()
        {
            SortedIntArray arr = new SortedIntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(4);
            arr.Add(5);
            Assert.Equal(1, arr[0]);
            Assert.Equal(2, arr[1]);
            Assert.Equal(3, arr[2]);
            Assert.Equal(4, arr[3]);
            Assert.Equal(5, arr[4]);
        }

        [Fact]
        public void ElementsWillBeModifiedAfterInsertion()
        {
            SortedIntArray arr = new SortedIntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(4);
            arr.Add(5);
            arr.Insert(0, 6);
            Assert.Equal(1, arr[0]);
            Assert.Equal(2, arr[1]);
            Assert.Equal(3, arr[2]);
            Assert.Equal(4, arr[3]);
            Assert.Equal(5, arr[4]);
            Assert.Equal(6, arr[5]);
        }

        [Fact]
        public void ElementsWillNotBeModifiedAfterInsertingLargestNumOnValidPosition()
        {
            SortedIntArray arr = new SortedIntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(4);
            arr.Add(5);
            arr.Insert(5, 6);
            Assert.Equal(1, arr[0]);
            Assert.Equal(2, arr[1]);
            Assert.Equal(3, arr[2]);
            Assert.Equal(4, arr[3]);
            Assert.Equal(5, arr[4]);
            Assert.Equal(6, arr[5]);
        }

        [Fact]
        public void ElementsWillBeModifiedAfterAssigningNewValue()
        {
            SortedIntArray arr = new SortedIntArray();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add(4);
            arr[0] = 5;
            Assert.Equal(2, arr[0]);
            Assert.Equal(3, arr[1]);
            Assert.Equal(4, arr[2]);
            Assert.Equal(5, arr[3]);
        }
    }
}