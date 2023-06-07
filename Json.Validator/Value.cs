using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Json
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var ws = new Many(new Any(" \n\r\t"));
            var value = new Choice(new String(), new Number(), new Text("true"), new Text("false"), new Text("null"));
            var element = new Sequence(ws, value, ws);
            var elements = new List(element, new Character(','));
            var member = new Sequence(ws, new String(), ws, new Character(':'), element);
            var members = new List(member, new Character(','));
            var obj = new Sequence(new Character('{'), ws, members, ws, new Character('}'));
            var array = new Sequence(new Character('['), ws, elements, ws, new Character(']'));
            var val = new Choice(new String(), new Number(), new Text("true"), new Text("false"), new Text("null"));
            val.Add(array);
            val.Add(obj);
            this.pattern = new Sequence(
                new Character('{'),
                ws,
                new List(new Sequence(new String(), new Character(':'), ws, val), new Sequence(new Character(','), ws)),
                ws,
                new Character('}'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
