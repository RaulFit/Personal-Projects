namespace MyPrefixTree
{
    public class PrefixTree<T> where T : notnull
    {
        private Node<T> Root;

        public PrefixTree()
        {
            Root = new Node<T>();
        }

        public void Insert(IEnumerable<T> word)
        {
            var current = Root;
            foreach (var ch in word)
            {
                if (!current.Children.ContainsKey(ch))
                {
                    current.Children[ch] = new Node<T>();
                }

                current = current.Children[ch];
            }

            current.IsEndOfWord = true;
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

        public bool startsWith(IEnumerable<T> prefix)
        {
            var current = Root;
            foreach (var ch in prefix)
            {
                if (!current.Children.ContainsKey(ch))
                {
                    return false;
                }

                current = current.Children[ch];
            }

            return true;
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