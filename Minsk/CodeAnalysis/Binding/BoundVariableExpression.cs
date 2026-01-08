
namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundVariableExpression : BoundExpression
    {
        private string _name;
        private Type _type;

        public BoundVariableExpression(string name, Type type)
        {
            _name = name;
            _type = type;
        }

        public string Name => _name;
        public override Type Type => _type;
        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
    }
}