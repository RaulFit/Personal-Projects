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
            var ws = new Optional(new Any("\b\n\r\t"));
            var value = new Choice(new String(), new Number(), new Text("true"), new Text("false"), new Text("null"));
            var element = new Sequence(ws, value, ws);
            var elements = new List(element, new Character(','));
            var member = new Sequence(ws, new String(), ws, new Character(':'), element);
            var members = new List(member, new Character(','));
            var obj = new Sequence(new Character('{'), ws, members, ws, new Character('}'));
            var array = new Sequence(new Character('['), ws, elements, ws, new Character(']'));
            var jsonValue = new Choice(value, obj, array);
            this.pattern = new Sequence(
                new Text("[\n"),
                ws,
                new OneOrMore(new Sequence(
                new Text("{\n"),
                ws,
                new List(
                    new Sequence(
                        new String(),
                        new Character(':'),
                        ws,
                        jsonValue),
                    new Text(",\n\t")),
                new Sequence(new Text("\n\t}"), new Optional(new Text(",\n"))))),
                new Text("\n]"));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
