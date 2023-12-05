namespace ExtensionMethods.Facts
{
    public class ExtensionMethodsFacts
    {
        [Fact]
        public void All_ShouldReturnTrueWhenAllElementsRespectTheCondition()
        {
            Assert.True(ExtensionMethods.All(new int[] { 2, 4, 6, 8, 10 }, e => e % 2 == 0));
        }

        [Fact]
        public void All_ShouldReturnFalseWhenNotAllElementsRespectTheCondition()
        {
            Assert.False(ExtensionMethods.All(new int[] { 2, 4, 6, 7, 10 }, e => e % 2 == 0));
        }

        [Fact]
        public void All_ShouldThrowExceptionWhenCollectionIsNull()
        {
            int[] arr = null;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.All(arr, e => e % 2 == 0));
            var ex = Assert.Throws<ArgumentNullException>(() => ExtensionMethods.All(arr, e => e % 2 == 0));
            Assert.Equal("source", ex.ParamName);
        }

        [Fact]
        public void All_ShouldThrowExceptionWhenFunctionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.All(new int[] { 2, 4, 6, 7, 10 }, null));
            var ex = Assert.Throws<ArgumentNullException>(() => ExtensionMethods.All(new int[] { 2, 4, 6, 7, 10 }, null));
            Assert.Equal("predicate", ex.ParamName);
        }

        [Fact]
        public void Any_ShouldReturnTrueWhenAnElementRespectsTheCondition()
        {
            Assert.True(ExtensionMethods.Any(new int[] { 1 , 5 ,7 ,8 ,13 }, e => e % 2 == 0));
        }

        [Fact]
        public void Any_ShouldReturnFalseWhenNoElementRespectsTheCondition()
        {
            Assert.False(ExtensionMethods.Any(new int[] { 1, 5, 7, 9, 13 }, e => e % 2 == 0));
        }

        [Fact]
        public void Any_ShouldThrowExceptionWhenCollectionIsNull()
        {
            int[] arr = null;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.Any(arr, e => e % 2 == 0));
        }

        [Fact]
        public void Any_ShouldThrowExceptionWhenFunctionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.Any(new int[] { 2, 4, 6, 7, 10 }, null));
        }

        [Fact]
        public void First_ShouldReturnFirstElementWithRequiredCondition()
        {
            Assert.Equal(8, ExtensionMethods.First(new int[] { 1, 3, 5 , 8 ,12 ,15, 16}, e => e % 2 == 0));
        }

        [Fact]
        public void First_ShouldThrowExceptionWhenNoElementWithRequiredConditionIsFound()
        {
            Assert.Throws<InvalidOperationException>(() => ExtensionMethods.First(new int[] { 1, 3, 5, 15}, e => e % 2 == 0));
        }

        [Fact]
        public void First_ShouldThrowExceptionWhenCollectionIsNull()
        {
            int[] arr = null;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.First(arr, e => e % 2 == 0));
        }

        [Fact]
        public void First_ShouldThrowExceptionWhenFunctionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.First(new int[] { 2, 4, 6, 7, 10 }, null));
        }

        [Fact]
        public void Select_WithValidInput_ShouldReturnExpectedResult()
        {
            var input = new List<int> { 1, 2, 3 };
            Func<int, int> selector = i => i + 1;
            var result = ExtensionMethods.Select(input, selector);
            Assert.Equal(new int[] { 2, 3, 4 }, result);
        }

        [Fact]
        public void Select_WithEmptyCollection_ShouldReturnEmptyCollection()
        {
            var input = new List<int>();
            Func<int, int> selector = i => i + 1;
            var result = ExtensionMethods.Select(input, selector);
            Assert.Equal(new LinkedList<int>(), result);
        }

        [Fact]
        public void SelectMany_WithValidInput_ReturnsExpectedResult()
        {
            var input = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 }, new List<int> { 5, 6 } };
            Func<List<int>, IEnumerable<int>> selector = list => list.Select(item => item * 2);
            var result = ExtensionMethods.SelectMany(input, selector);
            Assert.Equal(new int[] { 2, 4, 6, 8, 10, 12 }, result);
        }

        [Fact]
        public void SelectMany_WithEmptyCollection_ShouldReturnEmptyCollection()
        {
            var input = new List<List<int>>();
            Func<List<int>, IEnumerable<int>> selector = list => list.Select(item => item * 2);
            var result = ExtensionMethods.SelectMany(input, selector);
            Assert.Equal(new int[] { }, result);
        }

        [Fact]
        public void Where_NoElementsRespectPredicate_ShouldReturnAnEmptyCollection()
        {
            int[] arr = new int[] { 1, 3, 5, 7, 9, 11 };
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = ExtensionMethods.Where(arr, predicate);
            Assert.Equal(new int[] {}, result);
        }

        [Fact]
        public void Where_WithValidElements_ShouldReturnExpectedResult()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = ExtensionMethods.Where(arr, predicate);
            Assert.Equal(new int[] { 2, 4 ,6 }, result);
        }

        [Fact]
        public void ToDictionary_WithEmptyCollection_ShouldReturnEmptyDictionary()
        {
            int[] arr = { };
            Func<int, int> keySelector = i => i;
            Func<int, char> elementSelector = i => (char)(97 + i);
            Assert.Equal(new Dictionary<int, char>(), ExtensionMethods.ToDictionary(arr, keySelector, elementSelector));
        }

        [Fact]
        public void ToDictionary_WithValidCollection_ShouldReturnExpectedResult()
        {
            int[] arr = { 0, 1, 2, 3 };
            Func<int, int> keySelector = i => i;
            Func<int, char> elementSelector = i => (char)(97 + i);
            Assert.Equal(new Dictionary<int, char>() { { 0, 'a' }, { 1, 'b' }, { 2, 'c' }, { 3, 'd' }, }, ExtensionMethods.ToDictionary(arr, keySelector, elementSelector));;
        }

        [Fact]
        public void ToDictionary_CollectionIsNull_ShouldThrowArgumentNullException()
        {
            int[] arr = null;
            Func<int, int> keySelector = i => i;
            Func<int, char> elementSelector = i => (char)(97 + i);
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.ToDictionary(arr, keySelector, elementSelector));
        }

        [Fact]
        public void ToDictionary_KeySelectorIsNull_ShouldThrowArgumentNullException()
        {
            int[] arr = { 0, 1, 2, 3 };
            Func<int, int> keySelector = null;
            Func<int, char> elementSelector = i => (char)(97 + i);
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.ToDictionary(arr, keySelector, elementSelector));
        }

        [Fact]
        public void ToDictionary_ElementSelectorIsNull_ShouldThrowArgumentNullException()
        {
            int[] arr = { 0, 1, 2, 3 };
            Func<int, int> keySelector = i => i;
            Func<int, char> elementSelector = null;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.ToDictionary(arr, keySelector, elementSelector));
        }
    }
}