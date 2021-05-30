using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter
{
    public struct Token
    {
        public TokenType type;
        public object value;

        public Token(TokenType type, object value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public enum TokenType : int
    {
        Eof = -1,

        Int = 10,
        Float = 11,

        Plus = 20,
        Minus = 21,
        Mul = 22,
        Div = 23,

        OpenParen = 30,
        CloseParen = 31,
        OpenBrace = 32,
        CloseBrace = 33,
        Semi = 34,
        Dot = 35,

        Equals = 40,
        PlusEquals = 41,

        Keyword = 50,

    }
}
