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
    }
}