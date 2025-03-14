using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class NumberValidator : IPattern
    {
        private readonly IPattern pattern;

        public NumberValidator()
        {
            var minus = new OptionalValidator(new CharacterValidator('-'));
            var digit = new RangeValidator('0', '9');
            var digits = new OneOrMoreValidator(digit);
            var integer = new SequenceValidator(minus, new ChoiceValidator(new CharacterValidator('0'), digits));
            var sign = new OptionalValidator(new AnyValidator("+-"));
            var exponent = new OptionalValidator(new SequenceValidator(new AnyValidator("eE"), sign, digit));
            var fraction = new OptionalValidator(new SequenceValidator(new CharacterValidator('.'), digits));
            this.pattern = new SequenceValidator(integer, fraction, exponent);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
