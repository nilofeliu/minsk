using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis.Binding
{
    internal abstract class BoundNode
    {
        public abstract BoundNodeKind Kind { get; }

    }


    internal sealed class BoundVariableDeclaration : BoundStatement
    {
        public BoundVariableDeclaration(VariableSymbol variable, BoundExpression initializer)
        {
            Variable = variable;
            Initializer = initializer;
        }
        public override BoundNodeKind Kind => BoundNodeKind.VariableDeclaration;   
        public VariableSymbol Variable { get; }
        public BoundExpression Initializer { get; }

    }
}

