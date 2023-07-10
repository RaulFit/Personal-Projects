using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Txt : IPattern
    {
        private readonly string prefix;

        public Txt(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text) || !text.StartsWith(prefix))
            {
                return new Match(false, text);
            }

            return new Match(true, text[prefix.Length..]);
        }
    }
}
