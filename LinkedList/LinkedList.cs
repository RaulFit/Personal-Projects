using System.Collections;

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
                item.Prev = null;
                First = item;
                return;
            }
            Node<T> lastNode = GetLastNode();
            lastNode.Next = item;
            item.Prev = lastNode;
            Count++;
        }

        public void AddFirst(Node<T> nodeToAdd)
        {
            if (First is null)
            {
                First = nodeToAdd;
            }
            else
            {
                nodeToAdd.Next = First;
                First.Prev = nodeToAdd;
                First = nodeToAdd;
            }
            Count++;
        }

        public Node<T> GetLastNode()
        {
            Node<T>? temp = First;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            return temp;
        }

        public void Clear()
        {
            Node<T>? temp;
            while(First != null)
            {
                temp = First;
                First = First.Next;
                temp = null;
            }

            Count = 0;
        }

        public bool Contains(Node<T> item)
        {
            Node<T>? current = First;
            while(current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item.Value))
                {
                    return true;
                }
                current = current.Next;
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
            while (aux is not null)
            {
                if (EqualityComparer<T>.Default.Equals(aux.Value, valueToFind))
                {
                    return aux;
                }
                aux = aux.Next;
            }

            return null;
        }

        public bool Remove(Node<T> item)
        {
            var find = Find(item.Value);
            if (find is null)
            {
                return false;
            }

            var next = find.Next;
            var prev = find.Prev;
            if (prev is not null)
            {
                prev.Next = next;
            }
            if (next is not null)
            {
                next.Prev = prev;
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
            insertNode.Next = prev_node.Next;
            prev_node.Next = insertNode;
            insertNode.Prev = prev_node;
            if(insertNode.Next != null)
            {
                insertNode.Next.Prev = insertNode;
            }
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            Node<T>? currentNode = First;
            while (currentNode is not null)
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
