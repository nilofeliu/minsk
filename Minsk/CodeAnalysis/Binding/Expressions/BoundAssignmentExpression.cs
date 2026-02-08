using Minsk.CodeAnalysis.Binding.Kind;

namespace Minsk.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        private VariableSymbol _variable;
        private BoundExpression _expression;

        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression)
        {
            _variable = variable;
            _expression = expression;
        }

        public VariableSymbol Variable => _variable;
        public BoundExpression Expression => _expression;

        public override Type Type => Expression.Type;

        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }
}