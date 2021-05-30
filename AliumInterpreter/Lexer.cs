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

        private void Advance(int amt = 1)
        {
            readPos += amt;
            if (source.Length > readPos)
                currentChar = source[readPos];
            else
                eof = true;
        }

        private char Peek(int depth = 1)
        {
            int peekPos = readPos + depth;
            if (peekPos > source.Length - 1)
                return ' ';
            else return source[peekPos];
        }

        private bool PeekIsEof(int depth = 1)
        {
            return readPos + depth > source.Length - 1;
        }

        private void SkipWhitespace()
        {
            while (currentChar == ' ' && !eof)
            {
                Advance();
            }
        }

        private Token ReadNumeric()
        {
            string result = "";
            bool isDecimal = false;
            while ((char.IsDigit(currentChar) || (currentChar == '.' && !isDecimal)) && !eof)
            {
                if (currentChar == '.') isDecimal = true;
                result += currentChar;
                Advance();
            }
            if (isDecimal)
                return new Token(TokenType.Float, (int)float.Parse(result));
            return new Token(TokenType.Int, int.Parse(result));
        }

        private Token ReadIdentifier()
        {
            string result = "";
            while (char.IsLetterOrDigit(currentChar) && !eof)
            {
                result += currentChar;
                Advance();
            }
            return result switch
            {
                "int" => new Token(TokenType.Keyword, result),
                _ => throw new Exception("Unrecognized keyword: " + result),
            };
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
                else if (char.IsLetter(currentChar))
                    return ReadIdentifier();
                else if (char.IsDigit(currentChar))
                    return ReadNumeric();
                else if (currentChar == '+' && Peek() == '=')
                {
                    Advance(2);
                    return new Token(TokenType.PlusEquals, null);
                }
                else
                {
                    char c = currentChar;
                    Advance();
                    return c switch
                    {
                        '+' => new Token(TokenType.Plus, null),
                        '-' => new Token(TokenType.Minus, null),
                        '*' => new Token(TokenType.Mul, null),
                        '/' => new Token(TokenType.Div, null),
                        '(' => new Token(TokenType.OpenParen, null),
                        ')' => new Token(TokenType.CloseParen, null),
                        '{' => new Token(TokenType.OpenBrace, null),
                        '}' => new Token(TokenType.CloseBrace, null),
                        ';' => new Token(TokenType.Semi, null),
                        '.' => new Token(TokenType.Dot, null),
                        '=' => new Token(TokenType.Equals, null),
                        _ => throw new Exception("Unrecognized character: " + c),
                    };
                }
            }
            return new Token(TokenType.Eof, null);
        }
    }
}
