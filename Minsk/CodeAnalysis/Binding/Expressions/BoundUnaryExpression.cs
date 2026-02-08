using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Binding.Objects;

namespace Minsk.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public override Type Type => Op.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
        public BoundUnaryOperator Op { get; }
        public BoundExpression Operand { get; }

        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
        {
            Op = op;
            Operand = operand;
        }
    }
}

