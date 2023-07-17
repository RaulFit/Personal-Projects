using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class ObjEnum : IEnumerator
    {
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