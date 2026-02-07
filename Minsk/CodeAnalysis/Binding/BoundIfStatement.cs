using Minsk.CodeAnalysis.Binding;
using System.Collections.Immutable;

internal sealed class BoundIfStatement : BoundStatement
{
    public BoundIfStatement(
        BoundExpression condition,
        BoundStatement thenStatement,
        ImmutableArray<BoundElseIfClause> elseIfClauses,  // ADD THIS
        BoundStatement elseStatement)
    {
        Condition = condition;
        ThenStatement = thenStatement;
        ElseIfClauses = elseIfClauses;  // ADD THIS
        ElseStatement = elseStatement;
    }

    public override BoundNodeKind Kind => BoundNodeKind.IfStatement;
    public BoundExpression Condition { get; }
    public BoundStatement ThenStatement { get; }
    public ImmutableArray<BoundElseIfClause> ElseIfClauses { get; }  // ADD THIS
    public BoundStatement ElseStatement { get; }
}
