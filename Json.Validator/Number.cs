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
            var exponent = new Sequence(new Any("eE"), new Optional(new Any("-+")), new Range('1', '9'));
            var intNumber = new Sequence(minus, new Sequence(new Range('0', '9'), new Many(new Range('0', '9'))));
            var floatNumber = new Sequence(intNumber, new Character('.'), new OneOrMore(new Range('0', '9')), new Optional(exponent), new Many(new Range('0', '9')));
            var intNumberWithExponent = new Sequence(minus, intNumber, exponent, new Many(new Range('0', '9')));
            pattern = new Choice(new Choice(intNumber, floatNumber, intNumberWithExponent));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
