using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;

namespace Minsk.CodeAnalysis.Binding.Statements;

internal sealed class BoundExpressionStatement : BoundStatement
{
    public BoundExpressionStatement(BoundExpression expression)
    {
        Expression = expression;
    }

    public override BoundNodeKind Kind => BoundNodeKind.ExpressionStatement;
    public BoundExpression Expression { get; }

}