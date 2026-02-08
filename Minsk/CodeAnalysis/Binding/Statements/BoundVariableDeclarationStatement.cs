using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;

namespace Minsk.CodeAnalysis.Binding.Statements
{
    internal sealed class BoundVariableDeclarationStatement : BoundStatement
    {
        public BoundVariableDeclarationStatement(VariableSymbol variable, BoundExpression initializer)
        {
            Variable = variable;
            Initializer = initializer;
        }
        public override BoundNodeKind Kind => BoundNodeKind.VariableDeclaration;   
        public VariableSymbol Variable { get; }
        public BoundExpression Initializer { get; }

    }
}

