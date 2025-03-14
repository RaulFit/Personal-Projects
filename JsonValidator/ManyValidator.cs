using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ManyValidator : IPattern
    {
        private readonly IPattern pattern;

        public ManyValidator(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            IMatch match = new MatchValidator(true, text, text);

            while (match.Success())
            {
                match = pattern.Match(match.RemainingText());
            }

            return new MatchValidator(true, match.RemainingText(), match.ModifiedText());
        }
    }
}
