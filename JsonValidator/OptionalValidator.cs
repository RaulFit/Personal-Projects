using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class OptionalValidator : IPattern
    {
        private readonly IPattern pattern;

        public OptionalValidator(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            var match = pattern.Match(text);
            return new MatchValidator(true, match.RemainingText(), match.ModifiedText());
        }
    }
}
