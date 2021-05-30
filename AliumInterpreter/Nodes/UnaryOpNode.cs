using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class UnaryOpNode : ASTNode
    {
        public Token token;
        public ASTNode expr;

        public UnaryOpNode(Token token, ASTNode expr)
        {
            type = ASTNodeType.UnaryOpNode;
            this.token = token;
            this.expr = expr;
        }
    }
}
