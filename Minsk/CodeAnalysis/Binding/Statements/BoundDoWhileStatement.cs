using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;

namespace Minsk.CodeAnalysis.Binding.Statements
{
    internal class BoundDoWhileStatement : BoundStatement
    {
        public BoundDoWhileStatement(BoundExpression condition, BoundStatement body)
        {
            Condition = condition;
            Body = body;
        }

        public override BoundNodeKind Kind => BoundNodeKind.DoWhileStatement;
        public BoundExpression Condition { get; }
        public BoundStatement Body { get; }
    }
}