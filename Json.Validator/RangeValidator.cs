using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Range : IPattern
    {
        private readonly char start;
        private readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public IMatch Match(string text)
        {
            if (!string.IsNullOrEmpty(text) && text[0] >= this.start && text[0] <= this.end)
            {
                return new Match(true, text[1..], text[1..]);
            }

            return new Match(false, text, text);
        }
    }
}
