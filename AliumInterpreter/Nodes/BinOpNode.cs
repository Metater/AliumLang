using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class BinOpNode : ASTNode
    {
        public ASTNode left;
        public Token token;
        public ASTNode right;

        public BinOpNode(ASTNode left, Token token, ASTNode right)
        {
            nodeId = 1;
            this.left = left;
            this.token = token;
            this.right = right;
        }
    }
}
