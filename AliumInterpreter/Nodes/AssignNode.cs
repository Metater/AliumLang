using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class AssignNode : ASTNode
    {
        public ASTNode left;
        public Token token;
        public ASTNode right;

        public AssignNode(ASTNode left, Token token, ASTNode right)
        {
            type = ASTNodeType.AssignNode;
            this.left = left;
            this.token = token;
            this.right = right;
        }
    }
}
