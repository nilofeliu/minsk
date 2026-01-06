namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public override Type Type => Right.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
        public BoundExpression Left { get; }
        public BoundBinaryOperator Op { get; }
        public BoundExpression Right { get; }

        public BoundBinaryExpression(BoundExpression left,
            BoundBinaryOperator op,
            BoundExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }
    }
}

