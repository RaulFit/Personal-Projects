using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            var minus = new Opt(new Character('-'));
            var digit = new Range('0', '9');
            var digits = new OneOrMore(digit);
            var integer = new Sequence(minus, new Choice(new Character('0'), digits));
            var sign = new Opt(new Any("+-"));
            var exponent = new Opt(new Sequence(new Any("eE"), sign, digit));
            var fraction = new Opt(new Sequence(new Character('.'), digits));
            this.pattern = new Sequence(integer, fraction, exponent);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
