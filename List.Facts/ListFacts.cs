namespace GenericList.Facts
{
    public class ListFacts
    {
        [Fact]
        public void MethodCountReturnsZeroForAnEmptyArray()
        {
            var arr = new List<object>();
            Assert.Equal(0, arr.Count);
        }

        [Fact]
        public void MethodCountReturnsNumberOfElements()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(true);
            arr.Add("text");
            Assert.Equal(3, arr.Count);
        }

        [Fact]
        public void MethodElementReturnsTheElementFromSpecifiedIndex()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add("hello");
            arr.Add(3);
            Assert.Equal("hello", arr[1]);
        }

        [Fact]
        public void MethodElementWorksWhenArrayDoesNotContainIndex()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            Assert.Equal(-1, arr[6]);
        }

        [Fact]
        public void MethodSetElementModifiesElementAtSpecifiedIndex()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr[0] = true;
            Assert.Equal(true, arr[0]);
        }

        [Fact]
        public void MethodSetElementWorksWhenArrayDoesNotContainIndex()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr[6] = 5;
            Assert.Equal(-1, arr[6]);
        }

        [Fact]
        public void MethodContainsReturnsTrueWhenElementIsInArray()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add("text");
            arr.Add(3.21);
            Assert.True(arr.Contains("text"));
        }

        [Fact]
        public void MethodContainsReturnsFalseWhenElementIsNotInArray()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add("text");
            arr.Add(6.12);
            Assert.False(arr.Contains(6.10));
        }

        [Fact]
        public void MethodIndexOfReturnsIndexOfElement()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2.01);
            arr.Add(true);
            Assert.Equal(2, arr.IndexOf(true));
        }

        [Fact]
        public void MethodInsertDoesNotModifyArrayWhenIndexDoesNotExist()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(5, "hello");
            Assert.Equal(-1, arr[5]);
        }

        [Fact]
        public void MethodInsertModifiesArraySize()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(0, "element");
            Assert.Equal(4, arr.Count);
        }

        [Fact]
        public void MethodInsertModifiesElementAtSpecifiedIndex()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(0, 2.17);
            Assert.Equal(2.17, arr[0]);
        }

        [Fact]
        public void MethodInsertShiftsElementsFromSpecidiedIndexToTheRightWithOnePosition()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Insert(0, "firstElement");
            Assert.Equal("firstElement", arr[0]);
            Assert.Equal(1, arr[1]);
            Assert.Equal(2, arr[2]);
            Assert.Equal(3, arr[3]);
        }

        [Fact]
        public void MethodClearEmptiesTheArray()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(true);
            arr.Add(false);
            arr.Clear();
            Assert.Equal(0, arr.Count);
        }

        [Fact]
        public void MethodRemoveModifiesArraySize()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(true);
            arr.Add(3);
            arr.Add(1);
            arr.Remove(true);
            Assert.Equal(3, arr.Count);
        }

        [Fact]
        public void MethodRemoveReturnsTrueIfElementIsInArray()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(true);
            arr.Add(3);
            arr.Add(1);
            Assert.True(arr.Remove(3));
        }

        [Fact]
        public void MethodRemoveDeletesFirstAparitionOfElement()
        {
            var arr = new List<object>();
            arr.Add(true);
            arr.Add(2);
            arr.Add(3);
            arr.Add(true);
            arr.Remove(true);
            Assert.Equal(2, arr[0]);
            Assert.Equal(3, arr[1]);
            Assert.Equal(true, arr[2]);
        }

        [Fact]
        public void MethodRemoveAtDoesNotModifiyArrayIfSpecifiedIndexDoesNotExist()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add(2);
            arr.Add(3);
            arr.Add("four");
            arr.RemoveAt(6);
            Assert.Equal(4, arr.Count);
        }

        [Fact]
        public void MethodRemoveAtRemovesElementAtSpecifiedIndex()
        {
            var arr = new List<object>();
            arr.Add(1);
            arr.Add("two");
            arr.Add(3);
            arr.Add(true);
            arr.RemoveAt(1);
            Assert.Equal(1, arr[0]);
            Assert.Equal(3, arr[1]);
            Assert.Equal(true, arr[2]);
        }

        [Fact]
        public void MethodCopyToCopiesListElementsIntoSpecifiedArray()
        {
            var originalArr = new List<int>() { 3, 4, 5 };
            int[] specifiedArr = new int[5];
            specifiedArr[0] = 1;
            specifiedArr[1] = 2;
            originalArr.CopyTo(specifiedArr, 2);
            Assert.Equal(3, specifiedArr[2]);
            Assert.Equal(4, specifiedArr[3]);
            Assert.Equal(5, specifiedArr[4]);
        }
    }
}
