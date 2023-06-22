using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ValidLine : IPattern
    {
        private readonly IPattern pattern;

        public ValidLine()
        {
            this.pattern = new Sequence(new ValidName(), new Value(), new Optional(new Character(',')));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
