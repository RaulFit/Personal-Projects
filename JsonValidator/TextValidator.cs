using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class TextValidator : IPattern
    {
        private readonly string prefix;

        public TextValidator(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text) || !text.StartsWith(prefix))
            {
                return new MatchValidator(false, text, text);
            }

            return new MatchValidator(true, text[prefix.Length..], text[prefix.Length..]);
        }
    }
}
