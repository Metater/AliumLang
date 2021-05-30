using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class NoOpNode : ASTNode
    {

        public NoOpNode()
        {
            type = ASTNodeType.NoOpNode;
        }
    }
}
