using System;
using System.Collections.Generic;
using System.Text;

namespace AliumInterpreter.Nodes
{
    public class ASTNode
    {
        public ASTNodeType type;
    }

    public enum ASTNodeType
    {
        NumLeafNode = 0,
        BinOpNode = 1,
        UnaryOpNode = 2,
        CompoundNode = 3,
        AssignNode = 4,
        VarNode = 5,
        NoOpNode = 6,
    }
}
