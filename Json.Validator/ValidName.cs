using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ValidName : IPattern
    {
        private readonly IPattern pattern;

        public ValidName()
        {
            var ws = new Many(new Any(" \n\r\t"));
            this.pattern = new Sequence(ws, new String(), new Character(':'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}