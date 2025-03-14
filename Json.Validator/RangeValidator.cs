using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class RangeValidator : IPattern
    {
        private readonly char start;
        private readonly char end;

        public RangeValidator(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public IMatch Match(string text)
        {
            if (!string.IsNullOrEmpty(text) && text[0] >= this.start && text[0] <= this.end)
            {
                return new MatchValidator(true, text[1..], text[1..]);
            }

            return new MatchValidator(false, text, text);
        }
    }
}
