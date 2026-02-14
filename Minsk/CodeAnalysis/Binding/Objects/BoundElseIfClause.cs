using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Statements;
using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Kind;

internal sealed class BoundElseIfClause : SyntaxNode
{
    public BoundElseIfClause(BoundExpression condition, BoundStatement statement)
    {
        Condition = condition;
        Statement = statement;
    }
    public override SyntaxKind Kind => SyntaxKind.ElseIfClause;
    public BoundExpression Condition { get; }
    public BoundStatement Statement { get; }

}