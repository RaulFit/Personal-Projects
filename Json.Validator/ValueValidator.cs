using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class ValueValidator : IPattern
    {
        private readonly IPattern pattern;

        public ValueValidator()
        {
            var ws = new ManyValidator(new AnyValidator(" \n\r\t"));
            var value = new ChoiceValidator(new StringValidator(), new NumberValidator(), new TextValidator("true"), new TextValidator("false"), new TextValidator("null"));
            var element = new SequenceValidator(ws, value, ws);
            var elements = new ListValidator(element, new CharacterValidator(','));
            var member = new SequenceValidator(ws, new StringValidator(), ws, new CharacterValidator(':'), element);
            var members = new ListValidator(member, new CharacterValidator(','));
            var obj = new SequenceValidator(new CharacterValidator('{'), ws, members, ws, new CharacterValidator('}'));
            var array = new SequenceValidator(new CharacterValidator('['), ws, elements, ws, new CharacterValidator(']'));
            value.Add(array);
            value.Add(obj);
            this.pattern = element;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
