using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class AnyValidator : IPattern
    {
        private readonly string accepted;

        public AnyValidator(string accepted)
        {
            this.accepted = accepted;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text) || !this.accepted.Contains(text[0]))
            {
                return new MatchValidator(false, text, text);
            }

            return new MatchValidator(true, text[1..], text[1..]);
        }
    }
}
