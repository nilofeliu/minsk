namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public override Type Type => Right.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
        public BoundExpression Left { get; }
        public BoundBinaryOperatorKind Operatorkind { get; }
        public BoundExpression Right { get; }

        public BoundBinaryExpression(BoundExpression left,
            BoundBinaryOperatorKind operatorkind,
            BoundExpression right)
        {
            Left = left;
            Operatorkind = operatorkind;
            Right = right;
        }
    }
}

