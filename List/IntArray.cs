namespace List
{
    public class IntArray
    {
        private int[] array;

        public IntArray()
        {
            this.array = new int[0];
        }

        public int Count()
        {
            return array.Length;
        }

        public void Add(int element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[^1] = element;
        }

        public int Element(int index)
        {
            if(index < 0 || index >= array.Length)
            {
                return -1;
            }

            return array[index];
        }

        public void SetElement(int index, int element)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            array[index] = element;
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            if(index < 0 || index >= array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length + 1);
            ShiftRight(index);

            array[index] = element;
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
        }

        public void Remove(int element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index >= array.Length)
            {
                return;
            }

            ShiftLeft(index);

            Array.Resize(ref array, array.Length - 1);
        }

        private void ShiftLeft(int index)
        {
            for (int j = index; j < array.Length - 1; j++)
            {
                array[j] = array[j + 1];
            }
        }

        private void ShiftRight(int index)
        {
            for (int i = array.Length - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }
        }
    }
}
