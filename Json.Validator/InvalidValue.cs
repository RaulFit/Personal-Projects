﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class InvalidValue : IPattern
    {
        private readonly IPattern pattern;

        public InvalidValue()
        {
            var ws = new Many(new Any(" \n\r\t"));
            var value = new InvalidChoice(new String(), new Number(), new Text("true"), new Text("false"), new Text("null"));
            var element = new InvalidSequence(ws, value, ws);
            var elements = new InvalidList(element, new Character(','));
            var member = new InvalidSequence(ws, new String(), ws, new Character(':'), element);
            var members = new InvalidList(member, new Character(','));
            var obj = new InvalidSequence(new Character('{'), ws, members, ws, new Character('}'));
            var array = new InvalidSequence(new Character('['), ws, elements, ws, new Character(']'));
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
