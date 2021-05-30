using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class VarNode : ASTNode
    {
        public Token token;
        public object value;

        public VarNode(Token token)
        {
            type = ASTNodeType.VarNode;
            this.token = token;
            value = token.value;
        }
    }
}
