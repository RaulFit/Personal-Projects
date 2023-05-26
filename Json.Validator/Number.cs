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
            var minus = new Optional(new Character('-'));
            var oneNine = new Range('1', '9');
            var digit = new Choice(new Character('0'), oneNine);
            var digits = new OneOrMore(digit);
            var integer = new Choice(new Sequence(minus, digit), new Sequence(minus, oneNine, digits));
            var sign = new Optional(new Any("+-"));
            var exponent = new Choice(new Sequence(new Any("eE"), sign, digit), new Character((char)3));
            var fraction = new Choice(new Sequence(new Character('.'), digits), new Character((char)3), exponent);
            this.pattern = new Sequence(integer, fraction, exponent);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
