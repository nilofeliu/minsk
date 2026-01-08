
namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        private string _name;
        private BoundExpression _expression;

        public BoundAssignmentExpression(string name, BoundExpression expression)
        {
            _name = name;
            _expression = expression;
        }

        public string Name => _name;
        public BoundExpression Expression => _expression;

        public override Type Type => Expression.Type;

        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }
}