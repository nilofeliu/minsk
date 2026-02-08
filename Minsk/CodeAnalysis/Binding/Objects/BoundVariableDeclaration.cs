using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Binding.Statements;

namespace Minsk.CodeAnalysis.Binding.Objects
{
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

