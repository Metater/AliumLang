using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class NumLeafNode : ASTNode
    {
        public Token token;

        public NumLeafNode(Token token)
        {
            nodeId = 0;
            this.token = token;
        }
    }
}
