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

                current = value.CompareTo(current.value) < 0 ? current.left : current.right;
            }

            return current;
        }

        public void Insert(T value) => root = Insert(root, value);

        private Node<T>? Insert(Node<T>? root, T value)
        {
            if (root == null)
            {
                return new Node<T>(value);
            }

            else if (value.CompareTo(root.value) < 0)
            {
                root.left = Insert(root.left, value);
            }

            else if (value.CompareTo(root.value) > 0)
            {
                root.right = Insert(root.right, value);
            }

            else
            {
                throw new InvalidOperationException("Tree already contains the specified value");
            }

            return Balance(root);
        }

        private Node<T>? Balance(Node<T> root)
        {
            UpdateHeight(root);
            int balance = GetBalance(root);

            if (balance > 1)
            {
                if (GetHeight(root?.left?.left) - GetHeight(root?.left?.right) > 0)
                {
                    root = RotateRight(root);
                }

                if (GetHeight(root?.left?.left) - GetHeight(root?.left?.right) < 0)
                {
                    root.left = RotateLeft(root.left);
                    root = RotateRight(root);
                }
            }

            else if (balance < 1)
            {
                if (GetHeight(root?.right?.right) - GetHeight(root?.right?.left) > 0)
                {
                    root = RotateLeft(root);
                }

                if (GetHeight(root?.right?.right) - GetHeight(root?.right?.left) < 0)
                {
                    root.right = RotateRight(root.right);
                    root = RotateLeft(root);
                }
            }

            return root;
        }

        public int GetHeight(Node<T>? node) => node != null ? node.height : -1;

        private void UpdateHeight(Node<T> node) => node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        private int GetBalance(Node<T> node) => node == null ? 0 : GetHeight(node.left) - GetHeight(node.right);

        private Node<T> RotateLeft(Node<T>? y)
        {
            Node<T>? x = y.right;
            Node<T>? z = x?.left;
            x.left = y;
            y.right = z;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node<T> RotateRight(Node<T>? y)
        {
            Node<T>? x = y.left;
            Node<T>? z= x?.right;
            x.right = y;
            y.left = z;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
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