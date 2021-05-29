using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter
{
    public class Lexer
    {
        private string source;
        private int readPos = 0;
        private bool eof = false;
        private char currentChar;

        public Lexer(string source)
        {
            this.source = source;
            currentChar = source[0];
        }

        private void Advance()
        {
            readPos++;
            if (source.Length > readPos)
                currentChar = source[readPos];
            else
                eof = true;
        }

        private char Peek()
        {
            int peekPos = readPos + 1;
            if (peekPos > source.Length - 1)
                return ' ';
            else return source[peekPos];
        }

        private bool PeekIsEof()
        {
            return readPos + 1 > source.Length - 1;
        }

        private void SkipWhitespace()
        {
            while (currentChar == ' ' && !eof)
            {
                Advance();
            }
        }

        private int ReadNumeric()
        {
            string result = "";
            while (char.IsDigit(currentChar) && !eof)
            {
                result += currentChar;
                Advance();
            }
            return int.Parse(result);
        }

        public Token GetNextToken()
        {
            while (!eof)
            {
                if (currentChar == ' ')
                {
                    SkipWhitespace();
                    continue;
                }
                if (char.IsDigit(currentChar))
                    return new Token(0, ReadNumeric());
                switch (currentChar)
                {
                    case '+':
                        Advance();
                        return new Token(1, 0);
                    case '-':
                        Advance();
                        return new Token(2, 0);
                    case '*':
                        Advance();
                        return new Token(3, 0);
                    case '/':
                        Advance();
                        return new Token(4, 0);
                    case '(':
                        Advance();
                        return new Token(5, 0);
                    case ')':
                        Advance();
                        return new Token(6, 0);
                    default:
                        throw new Exception("Unrecognized character: " + currentChar);
                }
            }
            return new Token(-1, 0);
        }
    }
}
