using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class ObjEnum : IEnumerator
    {
        readonly protected object[] array;
        int count;
        int position;

        public ObjEnum(object[] arr, int c) 
        {
            this.array = arr;
            this.count = c;
            this.position = -1;
        }

        public bool MoveNext()
        {
            position++;
            return position < this.count;
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current { get => array[position]; }
    }
}
