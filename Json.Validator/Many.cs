using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Many : IPattern
    {
        private readonly IPattern pattern;

        public Many(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            IMatch match = new Match(true, text);
            while (!string.IsNullOrEmpty(match.RemainingText()) && pattern.Match(match.RemainingText()[0].ToString()).Success())
            {
                match = pattern.Match(match.RemainingText());
            }

            return match;
        }
    }
}
