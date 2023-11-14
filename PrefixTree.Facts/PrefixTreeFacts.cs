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
        public void Remove_ShouldNotModifyTreeWhenItDoesNotContainSpecifiedWord()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("dog");
            prefixTree.Insert("cat");
            prefixTree.Insert("horse");
            prefixTree.Remove("tiger");
            Assert.True(prefixTree.Search("dog"));
            Assert.True(prefixTree.Search("cat"));
            Assert.True(prefixTree.Search("horse"));
        }

        [Fact]
        public void Remove_ShouldRemoveAllNodesWhenTreeContainsOneWord()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("horse");
            prefixTree.Remove("horse");
            Assert.False(prefixTree.Search("horse"));
            Assert.False(prefixTree.startsWith("h"));
        }

        [Fact]
        public void Remove_ShouldNotRemoveAllNodesWhenTreeContainsSimilarWords()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("tree");
            prefixTree.Insert("treehouse");
            prefixTree.Remove("treehouse");
            Assert.False(prefixTree.Search("treehouse"));
            Assert.True(prefixTree.Search("tree"));
        }

        [Fact]
        public void Remove_ShouldNotRemoveAnyNodesWhenWordIsAPrefixOfAnotherWord()
        {
            var prefixTree = new PrefixTree<char>();
            prefixTree.Insert("tree");
            prefixTree.Insert("treehouse");
            prefixTree.Remove("tree");
            Assert.False(prefixTree.Search("tree"));
            Assert.True(prefixTree.Search("treehouse"));
        }
    }
}