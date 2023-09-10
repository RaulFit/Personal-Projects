using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedList
{
    public class Node<T>
    {
        internal T Value { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Prev { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}
