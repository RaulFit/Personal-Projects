using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class StringValidator : IPattern
    {
        private readonly IPattern pattern;

        public StringValidator()
        {
            var digit = new RangeValidator('0', '9');
            var hex = new ChoiceValidator(digit, new RangeValidator('A', 'F'), new RangeValidator('a', 'f'));
            var escape = new ChoiceValidator(new AnyValidator(@"\""\/\bfnrt"), new SequenceValidator(new CharacterValidator('u'), hex, hex, hex, hex));
            var character = new ChoiceValidator(new RangeValidator(' ', '!'), new RangeValidator('#', '['), new RangeValidator(']', '~'), new SequenceValidator(new CharacterValidator('\\'), escape));
            this.pattern = new SequenceValidator(new CharacterValidator('\"'), new ManyValidator(character), new CharacterValidator('\"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
