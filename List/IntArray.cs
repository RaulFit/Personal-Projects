using System.Xml.Linq;

namespace List
{
    public class IntArray
    {
        static void Main(string[] args)
        {
            IntArray a = new IntArray();
            a.Add(1);
            a.Add(2);
            a.Add(3);
            a.Add(4);
            a.Add(5);
            for(int i = 0; i < a.Count(); i++)
            {
                Console.Write(a.Element(i) + " ");
            }

            Console.WriteLine();    


            a.RemoveAt(1);

            for (int i = 0; i < a.Count(); i++)
            {
                Console.Write(a.Element(i) + " ");
            }

        }

        private int[] array;

	    public IntArray()
        {
            this.array = new int[0];
        }

        public void Add(int element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        public int Count()
        {
            return array.Length;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            array[index] = element;
        }

        public bool Contains(int element)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < array.Length; i++)
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
            Array.Resize(ref array, array.Length + 1);
            for(int i = array.Length - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }

            array[index] = element;
            
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
        }

        public void Remove(int element)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    for(int j = i; j < array.Length - 1; j++)
                    {
                        array[j] = array[j + 1];
                    }

                    break;
                }
            }

            Array.Resize(ref array, array.Length - 1);
        }

        public void RemoveAt(int index)
        {
            for(int j = index; j < array.Length - 1; j++)
            {
                array[j] = array[j + 1];
            }

            Array.Resize(ref array, array.Length - 1);
        }

    }
}