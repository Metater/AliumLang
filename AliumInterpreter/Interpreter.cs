using System;
using System.Collections.Generic;
using System.Text;
using AliumInterpreter.Nodes;

namespace AliumInterpreter
{
    public class Interpreter
    {
        private Lexer lexer;
        private Token currentToken;

        public Interpreter(string source)
        {
            lexer = new Lexer(source);
            currentToken = lexer.GetNextToken();
        }

        private void Eat(int tokenId)
        {
            if (currentToken.id == tokenId)
                currentToken = lexer.GetNextToken();
            else
                throw new Exception("Invalid syntax");
        }

        private ASTNode Factor()
        {
            Token token = currentToken;
            if (token.id == 1)
            {
                Eat(1);
                return new UnaryOpNode(token, Factor());
            }
            else if (token.id == 2)
            {
                Eat(2);
                return new UnaryOpNode(token, Factor());
            }
            else if (token.id == 0)
            {
                Eat(0);
                return new NumLeafNode(token);
            }
            else if (token.id == 5)
            {
                Eat(5);
                ASTNode node = Expr();
                Eat(6);
                return node;
            }
            else
            {
                throw new Exception("Invalid syntax");
            }
        }

        private ASTNode Term()
        {
            ASTNode node = Factor();
            while (currentToken.id == 3 || currentToken.id == 4)
            {
                Token token = currentToken;
                if (token.id == 3)
                {
                    Eat(3);
                }
                else if (token.id == 4)
                {
                    Eat(4);
                }
                node = new BinOpNode(node, token, Factor());
            }
            return node;
        }

        public ASTNode Expr()
        {
            ASTNode node = Term();
            while (currentToken.id == 1 || currentToken.id == 2)
            {
                Token token = currentToken;
                if (token.id == 1)
                {
                    Eat(1);
                }
                else if (token.id == 2)
                {
                    Eat(2);
                }
                node = new BinOpNode(node, token, Term());
            }
            return node;
        }

        public int Visit(ASTNode node)
        {
            switch (node.nodeId)
            {
                case 0:
                    return VisitNumLeafNode((NumLeafNode)node);
                case 1:
                    return VisitBinOpNode((BinOpNode)node);
                case 2:
                    return VisitUnaryOpNode((UnaryOpNode)node);
                default:
                    throw new Exception("Unknown node, cannot visit, id: " + node.nodeId);
            }
        }

        public int VisitNumLeafNode(NumLeafNode numLeaf)
        {
            return numLeaf.token.value;
        }

        public int VisitBinOpNode(BinOpNode binOpNode)
        {
            switch (binOpNode.token.id)
            {
                case 1:
                    return Visit(binOpNode.left) + Visit(binOpNode.right);
                case 2:
                    return Visit(binOpNode.left) - Visit(binOpNode.right);
                case 3:
                    return Visit(binOpNode.left) * Visit(binOpNode.right);
                case 4:
                    return Visit(binOpNode.left) / Visit(binOpNode.right);
                default:
                    throw new Exception("Unknown token id in binary op: " + binOpNode.token.id);
            }
        }

        public int VisitUnaryOpNode(UnaryOpNode unaryOpNode)
        {
            switch (unaryOpNode.token.id)
            {
                case 1:
                    return +Visit(unaryOpNode.expr);
                case 2:
                    return -Visit(unaryOpNode.expr);
                default:
                    throw new Exception("Unknown token id in unary op: " + unaryOpNode.token.id);
            }
        }

        public int Interpret()
        {
            return Visit(Expr());
        }
    }
}
