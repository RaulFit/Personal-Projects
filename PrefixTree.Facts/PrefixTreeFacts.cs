namespace MyPrefixTree.Facts
{
    public class PrefixTreeFacts
    {
        [Fact]
        public void Search_ShouldReturnFalseWhenTreeDoesNotContainSpecifiedWord()
        {
            var prefixTree = new PrefixTree<char>();
            Assert.False(prefixTree.Search("dog"));
        }

        [Fact]
        public void Insert_ShouldInsertSpecifiedWordInTree()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            Assert.True(prefixTree.Search("horse"));
        }

        [Fact]
        public void Search_ShouldReturnFalseWhenLastCharacterIsNotMarkedAsEndOfWord()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            Assert.False(prefixTree.Search("hor"));
        }

        [Fact]
        public void StartsWith_ShouldReturnFalseWhenNoWordStartsWithSpecifiedPrefix()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            Assert.False(prefixTree.startsWith("rab"));
        }

        [Fact]
        public void StartsWith_ShouldReturnTrueWhenAWordStartsWithSpecifiedPrefix()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            Assert.True(prefixTree.startsWith("hor"));
        }

        [Fact]
        public void Remove_ShouldReturnFalseWhenTreeDoesNotContainSpecifiedWord()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            Assert.False(prefixTree.Remove("animal"));
        }

        [Fact]
        public void Remove_ShouldReturnTrueAndRemoveWordWhenTreeContainsSpecifiedWord()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            Assert.True(prefixTree.Remove("horse"));
            Assert.False(prefixTree.Search("horse"));
        }
    }
}