using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class CompoundNode : ASTNode
    {
        public List<ASTNode> statements;

        public CompoundNode(List<ASTNode> statements)
        {
            type = ASTNodeType.CompoundNode;
            this.statements = statements;
        }
    }
}
