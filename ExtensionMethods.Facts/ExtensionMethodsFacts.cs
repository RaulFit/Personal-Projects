using System;
using System.Globalization;

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

        [Fact]
        public void Zip_WithEmptyCollections_ShouldReturnEmptyCollection()
        {
            int[] first = { };
            int[] second = { };
            Func<int, int, int> resultSelector = (i, j) => (i + j);
            Assert.Equal(new int[] { }, ExtensionMethods.Zip(first, second, resultSelector));
        }

        [Fact]
        public void Zip_SameLengthCollections_ShouldReturnExpectedResult()
        {
            int[] first = { 1, 2, 3, 4 };
            string[] second = { "one", "two", "three", "four"};
            Func<int, string, string> resultSelector = (i, j) => i + "-" + j;
            Assert.Equal(new string[] { "1-one", "2-two", "3-three", "4-four"}, ExtensionMethods.Zip(first, second, resultSelector));
        }

        [Fact]
        public void Zip_DifferentLengthCollections_ShouldReturnCollectionWithMinSize()
        {
            int[] first = { 1, 2, 3, 4, 5, 6, 7 };
            string[] second = { "one", "two", "three", "four" };
            Func<int, string, string> resultSelector = (i, j) => i + "-" + j;
            Assert.Equal(new string[] { "1-one", "2-two", "3-three", "4-four" }, ExtensionMethods.Zip(first, second, resultSelector));
        }

        [Fact]
        public void Aggregate_EmptyCollection_ShouldReturnSeed()
        {
            int[] arr = { };
            int seed = 5;
            Func<int, int, int> func = (i, j) => i * j;
            Assert.Equal(seed, ExtensionMethods.Aggregate(arr, seed, func));
        }

        [Fact]
        public void Aggregate_ValidCollection_ShouldReturnExpectedResult()
        {
            int[] arr = { 10, 20, 30, 40 };
            int seed = 5;
            Func<int, int, int> func = (i, j) => i * j;
            Assert.Equal(1200000, ExtensionMethods.Aggregate(arr, seed, func));
        }

        [Fact]
        public void Aggregate_NullCollection_ShouldThrowArgumentNullException()
        {
            int[] arr = null;
            int seed = 5;
            Func<int, int, int> func = (i, j) => i * j;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.Aggregate(arr, seed, func));
        }

        [Fact]
        public void Join_EmptyCollections_ShouldReturnEmptyCollection()
        {
            var outer = new List<int>();
            var inner = new List<int>();
            Func<int, int> outerKeySelector = e => e;
            Func<int, int> innerKeySelector = e => e;
            Func<int, int, string> resultSelector = (i, j) => i + "=" + j;
            Assert.Equal(new string[] { }, ExtensionMethods.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector));
        }

        [Fact]
        public void Join_ValidCollections_ShouldReturnExpectedResult()
        {
            var outer = new List<int>() { 1, 2, 3 };
            var inner = new List<int>() { 2, 3, 4 };
            Func<int, int> outerKeySelector = e => e;
            Func<int, int> innerKeySelector = e => e;
            Func<int, int, string> resultSelector = (i, j) => i + "-" + j;
            Assert.Equal(new string[] { "2-2", "3-3" }, ExtensionMethods.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector));
        }

        [Fact]
        public void Distinct_EmptyCollection_ShouldReturnEmptyCollection()
        {
            string[] list = { };
            Assert.Equal(new string[] { }, ExtensionMethods.Distinct(list, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Distinct_ValidCollection_ShouldReturnExpectedResult()
        {
            string[] list = { "apple", "Apple", "pear", "PEAR", "banana", "bAnaNA"};
            Assert.Equal(new string[] { "apple", "pear", "banana" }, ExtensionMethods.Distinct(list, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Union_EmptyCollections_ShouldReturnEmptyCollection()
        {
            string[] first = { };
            string[] second = { };
            Assert.Equal(new string[] { }, ExtensionMethods.Union(first, second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Union_ValidCollections_ShouldReturnExpectedResult()
        {
            string[] first = { "India", "USA", "Romania", "Canada", "Srilanka" };
            string[] second = { "India", "ROMANIA", "Canada", "France", "Japan" };
            Assert.Equal(new string[] { "India", "USA", "Romania", "Canada", "Srilanka", "France", "Japan" }, ExtensionMethods.Union(first, second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Intersect_EmptyCollections_ShouldReturnEmptyCollection()
        {
            string[] first = { };
            string[] second = { };
            Assert.Equal(new string[] { }, ExtensionMethods.Intersect(first, second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Intersect_ValidCollections_ShouldReturnCollectionWithCommonItems()
        {
            string[] first = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] second = { "India", "uk", "Canada", "France", "Japan" };
            Assert.Equal(new string[] { "India", "UK", "Canada" }, ExtensionMethods.Intersect(first, second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Except_EmptyCollections_ShouldReturnEmptyCollection()
        {
            string[] first = { };
            string[] second = { };
            Assert.Equal(new string[] { }, ExtensionMethods.Except(first, second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Except_ValidCollections_ShouldReturnCollectionWithItemsOnlyFromFirstCollection()
        {
            string[] first = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] second = { "India", "uk", "Canada", "France", "Japan" };
            Assert.Equal(new List<string>() { "USA", "Srilanka" }, ExtensionMethods.Except(first, second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void GroupBy_EmptyCollection_ShouldReturnEmptyCollection()
        {
            int[] source = { };
            var result = ExtensionMethods.GroupBy(source, x => x % 2, x => x * 2, (key, elements) => $"{key}: {string.Join(",", elements)}", EqualityComparer<int>.Default);
            Assert.Equal(new string[] { }, result);
        }

        [Fact]
        public void GroupBy_ValidCollection_ShouldReturnExpectedResult()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var result = ExtensionMethods.GroupBy(source, x => x % 2, x => x * 2, (key, elements) => $"{key}: {string.Join(",", elements)}", EqualityComparer<int>.Default);
            Assert.Collection(result, item => Assert.Equal("1: 2,6,10,14,18", item), item => Assert.Equal("0: 4,8,12,16,20", item));
        }

        [Fact]
        public void OrderBy_EmptyCollection_ShouldReturnEmptyCollection()
        {
            string[] names = { };
            Assert.Equal(new string[] { }, ExtensionMethods.OrderBy(names, name => name, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void OrderBy_ValidCollection_ShouldReturnExpectedResult()
        {
            string[] names = { "ALEX", "Alex", "David", "Rick", "Erik", "Mark" };
            Assert.Equal(new string[] { "Alex", "ALEX", "David", "Erik", "Mark", "Rick" }, ExtensionMethods.OrderBy(names, name => name, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ThenBy_EmptyCollection_ShouldReturnEmptyCollection()
        {
            string[] names = { };
            var orderedNames = ExtensionMethods.OrderBy(names, name => name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(new string[] { }, ExtensionMethods.ThenBy(orderedNames, name => name, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void ThenBy_ValidCollection_ShouldReturnExpectedResult()
        {
            string[] names = { "Alex", "ALEX", "David", "Rick", "Erik", "Mark" };
            var firstSort = ExtensionMethods.OrderBy(names, name => name, StringComparer.CurrentCulture);
            var secondSort = ExtensionMethods.ThenBy(firstSort, name => name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(new string[] { "Alex", "ALEX", "David", "Erik", "Mark", "Rick" }, secondSort);
        }
    }
}