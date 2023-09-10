using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace MyLinkedList
{
    public class LinkedList<T> : ICollection<Node<T>>
    {
        public Node<T>? First { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public void Add(Node<T> item)
        {
            if (First == null)
            {
                item.Next = item.Prev = item;
                First = item;
                return;
            }

            Node<T>? last = First.Prev;
            item.Next = First;
            First.Prev = item;
            item.Prev = last;
            last.Next = item;
            Count++;
        }

        public void AddFirst(Node<T> nodeToAdd)
        {
            if(First == null)
            {
                nodeToAdd.Next = nodeToAdd.Prev = nodeToAdd;
                First = nodeToAdd;
                return;
            }

            Node<T>? last = First.Prev;
            nodeToAdd.Next = First;
            nodeToAdd.Prev = last;
            last.Next = First.Prev = nodeToAdd;
            First = nodeToAdd;
            Count++;
        }

        public void Clear()
        {
            if(First != null)
            {
                Node<T>? temp;
                Node<T>? current = First.Next;
                while(current != First)
                {
                    temp = current.Next;
                    current = null;
                    current = temp;
                }

                First = null;
                Count = 0;
            }
        }

        public bool Contains(Node<T> item)
        {
            Node<T>? current = First;
            while(current.Next != First)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item.Value))
                {
                    return true;
                }
                current = current.Next;
            }

            if (EqualityComparer<T>.Default.Equals(current.Value, item.Value))
            {
                return true;
            }

            return false;
        }

        public void CopyTo(Node<T>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The specified array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The arrayIndex is less than 0");
            }

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            }

            for (int i = 0; i < Count; i++)
            {
                Node<T>? current = First;
                array[i + arrayIndex] = current;
                current = current.Next;
            }
        }

        public Node<T>? Find(T valueToFind)
        {
            var aux = First;
            while (aux.Next != First)
            {
                if (EqualityComparer<T>.Default.Equals(aux.Value, valueToFind))
                {
                    return aux;
                }
                aux = aux.Next;
            }

            if (EqualityComparer<T>.Default.Equals(aux.Value, valueToFind))
            {
                return aux;
            }

            return null;
        }

        public bool Remove(Node<T> item)
        {
            if(First == null)
            {
                return false;
            }

            Node<T>? current = First;
            Node<T>? prev1 = null;
            while(!EqualityComparer<T>.Default.Equals(current.Value, item.Value))
            {
                if(current.Next == First)
                {
                    return false;
                }
                prev1 = current;
                current = current.Next;
            }

            if(current.Next == First && prev1 == null)
            {
                First = null;
                Count--;
                return true;
            }

            if(current == First)
            {
                prev1 = First.Prev;
                First = First.Next;
                prev1.Next = First;
                First.Prev = prev1;
            }

            else if(current.Next == First)
            {
                prev1.Next = First;
                First.Prev = prev1;
            }

            else
            {
                Node<T>? temp = current.Next;
                prev1.Next = temp;
                temp.Prev = prev1;
            }

            Count--;
            return true;
        }

        public void InsertAfter(Node<T> prev_node, Node<T> insertNode)
        {
            if (prev_node == null)
            {
                throw new ArgumentNullException("The given previous node cannot be null");
            }

            Node<T>? temp = First;
            while(!EqualityComparer<T>.Default.Equals(temp.Value, prev_node.Value))
            {
                temp = temp.Next;
            }

            Node<T>? next = temp.Next;
            temp.Next = insertNode;
            insertNode.Prev = temp;
            insertNode.Next = next;
            next.Prev = insertNode;
            Count++;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            Node<T>? currentNode = First;

            if(currentNode== null)
            {
                yield return null;
            }

            while (currentNode.Next != First)
            {
                yield return currentNode;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
