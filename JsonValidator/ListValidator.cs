using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ListValidator : IPattern
    {
        private readonly IPattern pattern;

        public ListValidator(IPattern element, IPattern separator)
        {
            var separatorElement = new SequenceValidator(separator, element);
            this.pattern = new OptionalValidator(new SequenceValidator(element, new ManyValidator(separatorElement)));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
