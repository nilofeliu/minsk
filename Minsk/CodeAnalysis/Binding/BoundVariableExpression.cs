
namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundVariableExpression : BoundExpression
    {
        private VariableSymbol _variable;

        public BoundVariableExpression(VariableSymbol variable)
        {
            _variable = variable;
        }

        public VariableSymbol Variable => _variable;
        public override Type Type => _variable.Type;
        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
    }
}