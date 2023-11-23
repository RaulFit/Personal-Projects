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

        [Fact]
        public void Insert_ShouldInsertNodeWithSpecifiedValue()
        {
            var tree = new AVLTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            Assert.NotNull(tree.Find(1));
            Assert.NotNull(tree.Find(2));
            Assert.NotNull(tree.Find(3));
        }

        [Fact]
        public void Insert_ShouldThrowExceptionWhenTreeAlreadyContainsNodeWithSpecifiedValue()
        {
            var tree = new AVLTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            Assert.Throws<InvalidOperationException>(() => tree.Insert(2));
        }
    }
}