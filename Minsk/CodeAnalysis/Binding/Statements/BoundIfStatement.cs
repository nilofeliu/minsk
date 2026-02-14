using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Binding.Statements;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Binding.Statements;

internal sealed class BoundIfStatement : BoundStatement
{
    public BoundIfStatement(
        BoundExpression condition,
        BoundStatement thenStatement,
        BoundStatement elseStatement)
    {
        Condition = condition;
        ThenStatement = thenStatement;
        ElseStatement = elseStatement;
    }

    public override BoundNodeKind Kind => BoundNodeKind.IfStatement;
    public BoundExpression Condition { get; }
    public BoundStatement ThenStatement { get; }
    public BoundStatement ElseStatement { get; }
}
