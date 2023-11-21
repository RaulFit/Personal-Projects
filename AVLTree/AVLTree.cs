using System.Data;

namespace MyAVLTree
{
    public class AVLTree
    {
        private Node? root;

        public Node? Find(int value)
        {
            Node? current = root;
            while (current != null)
            {
                if (current.value == value)
                {
                    break;
                }

                current = current.value < value ? current.right : current.left;
            }

            return current;
        }

        public void Insert(int value) => root = Insert(value, root);

        public bool IsBalanced() => IsBalanced(root);

        private Node Insert(int value, Node? node)
        {
            if (node == null)
            {
                node = new Node(value);
                return node;
            }

            if (value < node.value)
            {
                node.left = Insert(value, node.left);
            }

            if (value > node.value)
            {
                node.right = Insert(value, node.right);
            }

            updateHeight(node);
            return Rotate(node);
        }

        private Node Rotate(Node node)
        {
            if (Height(node.left) - Height(node.right) > 1)
            {
                if (Height(node.left.left) - Height(node.left.right) > 0)
                {
                    return rightRotate(node);
                }

                if (Height(node.left.left) - Height(node.left.right) < 0)
                {
                    node.left = leftRotate(node.left);
                    return rightRotate(node);
                }
            }

            if (Height(node.left) - Height(node.right) < -1)
            {
                if (Height(node.right.left) - Height(node.right.right) < 0)
                {
                    return leftRotate(node);
                }

                if (Height(node.right.left) - Height(node.right.right) > 0)
                {
                    node.right = rightRotate(node.right);
                    return leftRotate(node);
                }
            }

            return node;
        }

        private bool IsBalanced(Node? node)
        {
            if (node == null)
            {
                return true;
            }

            return Math.Abs(Height(node.left) - Height(node.right)) <= 1 && IsBalanced(node.left) && IsBalanced(node.right);
        }

        private Node rightRotate(Node node)
        {
            Node? left = node.left;
            Node? leftRight = left?.right;

            left.right = node;
            node.left = leftRight;

            updateHeight(node);
            updateHeight(left);

            return left;
        }

        private Node leftRotate(Node node)
        {
            Node? right = node.right;
            Node? rightLeft = right?.left;

            right.left = node;
            node.right = rightLeft;

            updateHeight(right);
            updateHeight(node);

            return right;
        }

        private int Height(Node? node) => node == null ? -1 : node.height;
        
        private void updateHeight(Node node) => node.height = Math.Max(Height(node.left), Height(node.right)) + 1;
    }

    public sealed class Node
    {
        public int value;
        public int height;
        public Node? left;
        public Node? right;


        public Node(int value)
        {
            this.value = value;
        }
    }
}