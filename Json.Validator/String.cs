﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Str : IPattern
    {
        private readonly IPattern pattern;

        public Str()
        {
            var digit = new Range('0', '9');
            var hex = new Choice(digit, new Range('A', 'F'), new Range('a', 'f'));
            var escape = new Choice(new Any(@"\""\/\bfnrt"), new Sequence(new Character('u'), hex, hex, hex, hex));
            var character = new Choice(new Range(' ', '!'), new Range('#', '['), new Range(']', '~'), new Sequence(new Character('\\'), escape));
            this.pattern = new Sequence(new Character('\"'), new Many(character), new Character('\"'));
        }

        public IMatch Match(string? text)
        {
            return pattern.Match(text);
        }
    }
}
