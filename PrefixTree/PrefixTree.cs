namespace MyPrefixTree
{
    public class PrefixTree<T> where T : notnull
    {
        private Node<T> Root;

        public PrefixTree()
        {
            Root = new Node<T>();
        }

        public bool Search(IEnumerable<T> word)
        {
            var current = Root;
            foreach (var ch in word)
            {
                if (!current.Children.ContainsKey(ch))
                {
                    return false;
                }

                current = current.Children[ch];
            }

            return current.IsEndOfWord;
        }
    }

    public sealed class Node<T> where T : notnull
    {
        public Dictionary<T, Node<T>> Children;
        public bool IsEndOfWord;

        public Node()
        {
            Children = new Dictionary<T, Node<T>>();
            IsEndOfWord = false;
        }
    }
}