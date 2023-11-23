namespace MyAVLTree.Facts
{
    public class AVLTreeFacts
    {
        [Fact]
        public void Find_ShouldReturnNullWhenTreeDoesNotContainSpecifiedValue()
        {
            var tree = new AVLTree<int>();
            Assert.Null(tree.Find(1));
        }
    }
}