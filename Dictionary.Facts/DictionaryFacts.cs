namespace MyDictionary.Facts
{
    public class DictionaryFacts
    {
        [Fact]
        public void AddWithValues_ShouldAddNewElement()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            Assert.Equal(0, dictionary.buckets[1]);
            Assert.Equal("a", dictionary.elements[0].Value);
        }

        [Fact]
        public void AddWithValues_ShouldAddNewElements()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            Assert.Equal(2, dictionary.buckets[0]);
            Assert.Equal(0, dictionary.buckets[1]);
            Assert.Equal(1, dictionary.buckets[2]);
            Assert.Equal("a", dictionary.elements[0].Value);
            Assert.Equal("b", dictionary.elements[1].Value);
            Assert.Equal("c", dictionary.elements[2].Value);
        }

        [Fact]
        public void AddWithPair_ShouldAddNewElement()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(new KeyValuePair<int, string>(1, "a"));
            Assert.Equal(0, dictionary.buckets[1]);
            Assert.Equal("a", dictionary.elements[0].Value);
        }

        [Fact]
        public void AddWithPair_ShouldAddNewElements()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(new KeyValuePair<int, string>(1, "a"));
            dictionary.Add(new KeyValuePair<int, string>(2, "b"));
            dictionary.Add(new KeyValuePair<int, string>(10, "c"));
            Assert.Equal(2, dictionary.buckets[0]);
            Assert.Equal(0, dictionary.buckets[1]);
            Assert.Equal(1, dictionary.buckets[2]);
            Assert.Equal("a", dictionary.elements[0].Value);
            Assert.Equal("b", dictionary.elements[1].Value);
            Assert.Equal("c", dictionary.elements[2].Value);
        }

        [Fact]
        public void AddWithValues_BucketIndexIsNotEmpty_ShouldModifyNodeReference()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.Equal(2, dictionary.buckets[0]);
            Assert.Equal(0, dictionary.buckets[1]);
            Assert.Equal(4, dictionary.buckets[2]);
            Assert.Equal(-1, dictionary.buckets[3]);
            Assert.Equal(-1, dictionary.buckets[4]);
            Assert.Equal("a", dictionary.elements[0].Value);
            Assert.Equal("b", dictionary.elements[1].Value);
            Assert.Equal("c", dictionary.elements[2].Value);
            Assert.Equal("d", dictionary.elements[3].Value);
            Assert.Equal("e", dictionary.elements[4].Value);
        }

        [Fact]
        public void AddWithPairs_BucketIndexIsNotEmpty_ShouldModifyNodeReference()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(new KeyValuePair<int, string>(1, "a"));
            dictionary.Add(new KeyValuePair<int, string>(2, "b"));
            dictionary.Add(new KeyValuePair<int, string>(10, "c"));
            dictionary.Add(new KeyValuePair<int, string>(7, "d"));
            dictionary.Add(new KeyValuePair<int, string>(12, "e"));
            Assert.Equal(2, dictionary.buckets[0]);
            Assert.Equal(0, dictionary.buckets[1]);
            Assert.Equal(4, dictionary.buckets[2]);
            Assert.Equal(-1, dictionary.buckets[3]);
            Assert.Equal(-1, dictionary.buckets[4]);
            Assert.Equal("a", dictionary.elements[0].Value);
            Assert.Equal("b", dictionary.elements[1].Value);
            Assert.Equal("c", dictionary.elements[2].Value);
            Assert.Equal("d", dictionary.elements[3].Value);
            Assert.Equal("e", dictionary.elements[4].Value);
        }

        [Fact]
        public void SearchByKey_ShouldFindElementWithSpecifiedKey()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(0, "a");
            dictionary.Add(1, "b");
            dictionary.Add(2, "c");
            Assert.Equal("a", dictionary[0]);
            Assert.Equal("b", dictionary[1]);
            Assert.Equal("c", dictionary[2]);
        }

        [Fact]
        public void RemoveWithKey_KeyIsNotFound_ShouldReturnFalse()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(0, "a");
            dictionary.Add(1, "b");
            dictionary.Add(2, "c");
            Assert.False(dictionary.Remove(3));
        }

        [Fact]
        public void RemoveWithKey_FirstElemInBucket_ShoulRemoveElementAndReturnTrue()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.True(dictionary.Remove(12));
        }

        [Fact]
        public void RemoveWithKey_RandomElemInBucket_ShoulRemoveElementAndReturnTrue()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.True(dictionary.Remove(7));
        }

        [Fact]
        public void RemoveWithPair_KeyIsNotFound_ShouldReturnFalse()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(new KeyValuePair<int, string>(0, "a"));
            dictionary.Add(new KeyValuePair<int, string>(1, "b"));
            dictionary.Add(new KeyValuePair<int, string>(2, "c"));
            Assert.False(dictionary.Remove(3));
        }

        [Fact]
        public void ContainsKey_ShouldReturnTrueWhenKeyIsInDictionary()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.True(dictionary.ContainsKey(10));
        }

        [Fact]
        public void ContainsKey_ShouldReturnFalseWhenKeyIsNotInDictionary()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.False(dictionary.ContainsKey(20));
        }

        [Fact]
        public void Contains_ShouldReturnTrueWhenElementIsInDictionary()
        {
            var dictionary = new Dictionary<int, string>(5);
            var elem = new KeyValuePair<int, string>(1, "a");
            dictionary.Add(elem);
            dictionary.Add(new KeyValuePair<int, string>(2, "b"));
            dictionary.Add(new KeyValuePair<int, string>(10, "c"));
            dictionary.Add(new KeyValuePair<int, string>(7, "d"));
            dictionary.Add(new KeyValuePair<int, string>(12, "e"));
            Assert.True(dictionary.Contains(elem));
        }

        [Fact]
        public void Contains_ShouldReturnFalseWhenElementIsNotInDictionary()
        {
            var dictionary = new Dictionary<int, string>(5);
            var elem = new KeyValuePair<int, string>(1, "a");
            dictionary.Add(new KeyValuePair<int, string>(2, "b"));
            dictionary.Add(new KeyValuePair<int, string>(10, "c"));
            dictionary.Add(new KeyValuePair<int, string>(7, "d"));
            dictionary.Add(new KeyValuePair<int, string>(12, "e"));
            Assert.False(dictionary.Contains(elem));
        }

        [Fact]
        public void RemoveWithPair_FirstElemInBucket_ShoulRemoveElementAndReturnTrue()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(new KeyValuePair<int, string>(1, "a"));
            dictionary.Add(new KeyValuePair<int, string>(2, "b"));
            dictionary.Add(new KeyValuePair<int, string>(10, "c"));
            dictionary.Add(new KeyValuePair<int, string>(7, "d"));
            dictionary.Add(new KeyValuePair<int, string>(12, "e"));
            Assert.True(dictionary.Remove(12));
        }

        [Fact]
        public void RemoveWithPair_RandomElemInBucket_ShoulRemoveElementAndReturnTrue()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(new KeyValuePair<int, string>(1, "a"));
            dictionary.Add(new KeyValuePair<int, string>(2, "b"));
            dictionary.Add(new KeyValuePair<int, string>(10, "c"));
            dictionary.Add(new KeyValuePair<int, string>(7, "d"));
            dictionary.Add(new KeyValuePair<int, string>(12, "e"));
            Assert.True(dictionary.Remove(7));
        }

        [Fact]
        public void DictionaryUsesTheFreePositionWhenAddingNewElement()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            dictionary.Remove(7);
            dictionary.Remove(1);
            Assert.Equal(0, dictionary.freeIndex);
            dictionary.Add(17, "f");
            Assert.Equal("f", dictionary.elements[0].Value);
            Assert.Equal(3, dictionary.freeIndex);
            dictionary.Add(3, "g");
            Assert.Equal("g", dictionary.elements[3].Value);
            Assert.Equal(-1, dictionary.freeIndex);
        }

        [Fact]
        public void TryGetValue_ValueIsInDictionary_ShouldReturnTrue()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.True(dictionary.TryGetValue(2, out string val));
        }

        [Fact]
        public void TryGetValue_ValueIsNotInDictionary_ShouldReturnFalse()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            Assert.False(dictionary.TryGetValue(15, out string val));
        }
    }
}