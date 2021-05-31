using System;
using System.Collections.Generic;
using System.Text;
using AliumInterpreter.Nodes;

namespace AliumInterpreter
{
    public class Parser
    {
        private Lexer lexer;
        private Token currentToken;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            currentToken = lexer.GetNextToken();
        }

        private void Eat(TokenType type)
        {
            if (currentToken.type == type)
                currentToken = lexer.GetNextToken();
            else
                throw new Exception("Invalid syntax");
        }

        public ASTNode Program()
        {
            ASTNode node = CompoundStatement();
            return node;
        }

        private ASTNode CompoundStatement()
        {
            Eat(TokenType.OpenBrace);
            List<ASTNode> nodes = StatementList();
            Eat(TokenType.CloseBrace);
            ASTNode root = new CompoundNode(nodes);
            return root;
        }

        private List<ASTNode> StatementList()
        {
            ASTNode node = Statement();
            List<ASTNode> statements = new List<ASTNode>();
            statements.Add(node);
            while (currentToken.type == TokenType.Semi)
            {
                Eat(TokenType.Semi);
                statements.Add(Statement());
            }
            if (currentToken.type == TokenType.Keyword)
                throw new Exception("Found unexpected keyword: " + ((string)currentToken.value));
            return statements;
        }

        private ASTNode Statement()
        {
            ASTNode node;
            if (currentToken.type == TokenType.OpenBrace)
                node = CompoundStatement();
            else if (currentToken.type == TokenType.Keyword)
                node = AssignmentStatement();
            else
                node = Empty();
            return node;
        }

        private ASTNode AssignmentStatement()
        {
            ASTNode left = Variable();
            Token token = currentToken;
            Eat(TokenType.Equals);
            ASTNode right = Expr();
            ASTNode node = new AssignNode(left, token, right);
            return node;
        }

        private ASTNode Variable()
        {
            ASTNode node = new VarNode(currentToken);
            Eat(TokenType.Keyword);
            return node;
        }

        private ASTNode Empty()
        {
            return new NoOpNode();
        }

        public ASTNode Expr()
        {
            ASTNode node = Term();
            while (currentToken.type == TokenType.Plus || currentToken.type == TokenType.Minus)
            {
                Token token = currentToken;
                if (token.type == TokenType.Plus)
                {
                    Eat(TokenType.Plus);
                }
                else if (token.type == TokenType.Minus)
                {
                    Eat(TokenType.Minus);
                }
                node = new BinOpNode(node, token, Term());
            }
            return node;
        }

        private ASTNode Term()
        {
            ASTNode node = Factor();
            while (currentToken.type == TokenType.Mul || currentToken.type == TokenType.Div)
            {
                Token token = currentToken;
                if (token.type == TokenType.Mul)
                {
                    Eat(TokenType.Mul);
                }
                else if (token.type == TokenType.Div)
                {
                    Eat(TokenType.Div);
                }
                node = new BinOpNode(node, token, Factor());
            }
            return node;
        }

        private ASTNode Factor()
        {
            Token token = currentToken;
            if (token.type == TokenType.Plus)
            {
                Eat(TokenType.Plus);
                return new UnaryOpNode(token, Factor());
            }
            else if (token.type == TokenType.Minus)
            {
                Eat(TokenType.Minus);
                return new UnaryOpNode(token, Factor());
            }
            else if (token.type == TokenType.Int)
            {
                Eat(TokenType.Int);
                return new NumLeafNode(token);
            }
            else if (token.type == TokenType.OpenParen)
            {
                Eat(TokenType.OpenParen);
                ASTNode node = Expr();
                Eat(TokenType.CloseParen);
                return node;
            }
            else
            {
                ASTNode node = Variable();
                return node;
            }
        }

        public ASTNode Parse()
        {
            ASTNode node = Program();
            if (currentToken.type != TokenType.Eof)
                throw new Exception("Expected Eof, instead got: " + currentToken.type.ToString());
            return node;
        }
    }
}
