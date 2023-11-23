namespace MyAVLTree
{
    public class AVLTree<T> where T : IComparable<T>
    {
        public Node<T>? root;

        public Node<T>? Find(T value)
        {
            Node<T>? current = root;
            while (current != null)
            {
                if (current.value.Equals(value))
                {
                    break;
                }

                else if (value.CompareTo(current.value) < 0)
                {
                    current = current.left;
                }

                else
                {
                    current = current.right;
                }
            }

            return current;
        }

        public sealed class Node<T> where T : IComparable<T>
        {
            public T value;
            public int height;
            public Node<T>? left;
            public Node<T>?right;

            public Node(T value)
            {
                this.value = value;
            }
        }
    }
}