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
    }
}