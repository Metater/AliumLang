using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter
{
    public struct Token
    {
        public int id;
        public int value;

        public Token(int id, int value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
