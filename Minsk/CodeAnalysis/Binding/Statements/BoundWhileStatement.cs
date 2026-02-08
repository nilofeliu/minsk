using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;

namespace Minsk.CodeAnalysis.Binding.Statements
{
    internal sealed class BoundWhileStatement: BoundStatement
    {
        public BoundWhileStatement(BoundExpression condition, BoundStatement body)
        {
            Condition = condition;
            Body = body;
        }

        public BoundExpression Condition { get; }
        public BoundStatement Body { get; }

        public override BoundNodeKind Kind => BoundNodeKind.WhileStatement;
    }
}

