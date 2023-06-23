using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ObjectValues : IPattern
    {
        private readonly IPattern pattern;

        public ObjectValues()
        {
            var ws = new Many(new Any(" \n\r\t"));
            this.pattern = new List(new Sequence(ws, new NameValue(), ws), new Character(','));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
