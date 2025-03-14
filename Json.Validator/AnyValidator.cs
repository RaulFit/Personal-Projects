using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Any : IPattern
    {
        private readonly string accepted;

        public Any(string accepted)
        {
            this.accepted = accepted;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text) || !this.accepted.Contains(text[0]))
            {
                return new Match(false, text, text);
            }

            return new Match(true, text[1..], text[1..]);
        }
    }
}
