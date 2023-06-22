using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ValidObject
    {
        private readonly IPattern pattern;

        public ValidObject()
        {
            var ws = new Many(new Any(" \n\r\t"));
            this.pattern = new Sequence(ws, new ValidName(), ws, new Character('{'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
