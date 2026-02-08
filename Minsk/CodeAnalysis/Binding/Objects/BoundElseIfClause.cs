using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Statements;

internal sealed class BoundElseIfClause
{
    public BoundElseIfClause(BoundExpression condition, BoundStatement statement)
    {
        Condition = condition;
        Statement = statement;
    }

    public BoundExpression Condition { get; }
    public BoundStatement Statement { get; }
}