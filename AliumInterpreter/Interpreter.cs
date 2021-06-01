using System;
using System.Collections.Generic;
using System.Text;
using AliumInterpreter.Nodes;

namespace AliumInterpreter
{
    public class Interpreter
    {
        private Lexer lexer;
        private Parser parser;

        public Interpreter(string source)
        {
            lexer = new Lexer(source);
            parser = new Parser(lexer);
        }

        public object Visit(ASTNode node)
        {
            switch (node.type)
            {
                case ASTNodeType.NumLeafNode:
                    return VisitNumLeafNode((NumLeafNode)node);
                case ASTNodeType.BinOpNode:
                    return VisitBinOpNode((BinOpNode)node);
                case ASTNodeType.UnaryOpNode:
                    return VisitUnaryOpNode((UnaryOpNode)node);
                default:
                    throw new Exception("Unknown node, cannot visit node: " + node.type.ToString());
            }
        }

        public int VisitNumLeafNode(NumLeafNode numLeaf)
        {
            return (int)numLeaf.token.value;
        }

        public int VisitBinOpNode(BinOpNode binOpNode)
        {
            switch (binOpNode.token.type)
            {
                case TokenType.Plus:
                    return (int)Visit(binOpNode.left) + (int)Visit(binOpNode.right);
                case TokenType.Minus:
                    return (int)Visit(binOpNode.left) - (int)Visit(binOpNode.right);
                case TokenType.Mul:
                    return (int)Visit(binOpNode.left) * (int)Visit(binOpNode.right);
                case TokenType.Div:
                    return (int)Visit(binOpNode.left) / (int)Visit(binOpNode.right);
                default:
                    throw new Exception("Unknown token id in binary op: " + binOpNode.token.type.ToString());
            }
        }

        public int VisitUnaryOpNode(UnaryOpNode unaryOpNode)
        {
            switch (unaryOpNode.token.type)
            {
                case TokenType.Plus:
                    return +(int)Visit(unaryOpNode.expr);
                case TokenType.Minus:
                    return -(int)Visit(unaryOpNode.expr);
                default:
                    throw new Exception("Unknown token id in unary op: " + unaryOpNode.token.type.ToString());
            }
        }

        public void VisitCompoundNode(CompoundNode node)
        {
            foreach (ASTNode child in node.statements)
            {
                Visit(child);
            }
        }

        public int Interpret()
        {
            ASTNode program = parser.Parse();
            return (int)Visit(program);
        }
    }
}
