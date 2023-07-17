using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class ObjEnum : IEnumerator
    {
        static void Main(string[] args)
        {
            ObjectArray a = new ObjectArray() { 1, 2, 3, true, false };
            foreach (object o in a)
            {
                Console.WriteLine(o);
            }
        }

        ObjectArray array;
        int position;

        public ObjEnum(ObjectArray arr)
        {
            array = arr;
            this.position = -1;
        }

        public bool MoveNext()
        {
            position++;
            return position < array.Count;
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current { get => array[position]; }
    }
}