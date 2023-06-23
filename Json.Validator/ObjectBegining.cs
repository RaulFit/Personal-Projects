using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ObjectBegining : IPattern
    {
        private readonly IPattern pattern;

        public ObjectBegining()
        {
            var ws = new Many(new Any(" \n\r\t"));
            this.pattern = new Sequence(ws, new String(), new Character(':'), ws, new Character('{'), ws);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
