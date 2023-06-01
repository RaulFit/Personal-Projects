using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var ws = new Optional(new Choice(new Character('\u0020'), new Character('\u000A'), new Character('\u000D'), new Character('\u0009')));
            var value = new Choice(new String(), new Number(), new Text("true"), new Text("false"), new Text("null"));
            var element = new Sequence(ws, value, ws);
            var elements = new List(element, new Text(", "));
            var member = new Sequence(ws, new String(), ws, new Character(':'), element);
            var members = new List(member, new Text(",\n"));
            var obj = new Choice(new Sequence(new Character('{'), ws, new Character('}')), new Sequence(new Character('{'), members, new Character('}')));
            var array = new Choice(new Sequence(new Character('['), ws, new Character(']')), new Sequence(new Character('['), elements, new Character(']')));
            this.pattern = new Choice(obj, array, value);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
