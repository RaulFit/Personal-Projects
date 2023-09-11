using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedList
{
    public class Node<T>
    {
        public T Data;
        public Node<T>? Next;
        public Node<T>? Prev;

        public Node(T value)
        {
            Next = Prev = null;
            Data = value;
        }
    }
}
