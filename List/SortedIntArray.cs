namespace GenericList
{
    public class SortedIntArray : IntArray
    {
        public override void Add(int element)
        {
            Realocate();
            array[Count] = element;
            Count++;
            Array.Sort(array, 0, Count);
        }

        public override void Insert(int index, int element)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            Realocate();
            ShiftRight(index);
            array[index] = element;
            Count++;
            Array.Sort(array, 0, Count);
        }

        public override int this[int index]
        {
            get => array[index];
            set
            {
                array[index] = value;
                Array.Sort(array, 0, Count);
            }
        }

    }
}
