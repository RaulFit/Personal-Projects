﻿using System.Collections;

namespace List
{
    public class ObjectArray : IEnumerable
    {
        static void Main(string[] args)
        {
            ObjectArray arr = new ObjectArray { 1, true, "text", 2.1, 5.5};
            foreach(object o in arr)
            {
                Console.WriteLine(o);
            }
        }

        private object[] array;
        protected int count;

        public ObjectArray()
        {
            this.array = new object[4];
            this.count = 0;
        }

        public int Count { get; protected set; }

        public void Add(object element)
        {
            Realocate();
            array[Count] = element;
            Count++;
        }

        public object this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public bool Contains(object element)
        {
            return array.Contains(element);
        }

        public int IndexOf(object element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, object element)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            Realocate();
            ShiftRight(index);
            array[index] = element;
            Count++;
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
            Count = 0;
        }

        public void Remove(object element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            ShiftLeft(index);
            Count--;
        }

        protected void ShiftLeft(int index)
        {
            for (int j = index; j < array.Length - 1; j++)
            {
                array[j] = array[j + 1];
            }
        }

        protected void ShiftRight(int index)
        {
            for (int i = array.Length - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }
        }

        protected void Realocate()
        {
            if (Count == array.Length)
            {
                Array.Resize(ref array, array.Length * 2);
            }
        }

        public IEnumerator GetEnumerator()
        {
            object[] valuesArray = new object[Count];

            for(int i = 0; i < Count; i++)
            {
                valuesArray[i] = array[i];
            }

            return new ObjEnum(valuesArray);
        }
    }
}
