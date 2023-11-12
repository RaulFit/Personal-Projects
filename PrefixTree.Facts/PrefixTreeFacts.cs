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
    }
}