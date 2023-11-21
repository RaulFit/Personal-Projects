namespace MyAVLTree.Facts
{
    public class AVLTreeFacts
    {
        [Fact]
        public void Find_TreeDoesNotContainNode_ShouldReturnFalse()
        {
            var tree = new AVLTree();
            Assert.Null(tree.Find(3));
        }

        [Fact]
        public void Insert_ShouldCreateAndInsertNode()
        {
            var tree = new AVLTree();
            tree.Insert(3);
            Assert.NotNull(tree.Find(3));
        }

        [Fact]
        public void Insert_WorkWhenAddingMultipleNodes()
        {
            var tree = new AVLTree();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            Assert.NotNull(tree.Find(1));
            Assert.NotNull(tree.Find(2));
            Assert.NotNull(tree.Find(3));
        }

        [Fact]
        public void TreeSelfBalancesWhenInsertingMaxValueNodes()
        {
            var tree = new AVLTree();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
            Assert.True(tree.IsBalanced());
        }
    }
}