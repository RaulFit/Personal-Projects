using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class CharacterValidator : IPattern
    {
        readonly char pattern;

        public CharacterValidator(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text) || text[0] != pattern)
            {
                return new MatchValidator(false, text, text);
            }

            return new MatchValidator(true, text[1..], text[1..]);
        }
    }
}
