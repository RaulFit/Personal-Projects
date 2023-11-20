using System.Data;

namespace MyAVLTree
{
    public class AVLTree
    {
        private Node? Root;

        public Node? Find(int key)
        {
            Node? current = Root;
            while (current != null)
            {
                if (current.Key == key)
                {
                    break;
                }

                current = current.Key < key ? current.Right : current.Left;
            }

            return current;
        }
    }

    public sealed class Node
    {
        public int Key;
        public int Height;
        public Node? Left;
        public Node? Right;


        public Node(int key)
        {
            Key = key;
        }
    }
}