using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Binding.Statements;
using System.Collections.Immutable;

internal sealed class BoundIfStatement : BoundStatement
{
    public BoundIfStatement(
        BoundExpression condition,
        BoundStatement thenStatement,
        ImmutableArray<BoundElseIfClause> elseIfStatements,  
        BoundStatement elseStatement)
    {
        Condition = condition;
        ThenStatement = thenStatement;
        ElseIfStatements = elseIfStatements;  
        ElseStatement = elseStatement;
    }

    public override BoundNodeKind Kind => BoundNodeKind.IfStatement;
    public BoundExpression Condition { get; }
    public BoundStatement ThenStatement { get; }
    public ImmutableArray<BoundElseIfClause> ElseIfStatements { get; }  
    public BoundStatement ElseStatement { get; }
}
