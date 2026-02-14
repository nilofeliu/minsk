using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Binding.Statements;
using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Statement;
using System.Collections.Immutable;

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

internal sealed class BoundSwitchStatement : BoundStatement
{
    public BoundSwitchStatement(
        SyntaxToken switchKeyword,
        BoundExpression pattern,
        ImmutableArray<SwitchCaseStatementSyntax> cases,
        SwitchCaseStatementSyntax defaultCase)
    {
        Pattern = pattern;
        Cases = cases;
        DefaultCase = defaultCase;
    }

    public override BoundNodeKind Kind => BoundNodeKind.SwitchStatement;
    public BoundExpression Pattern { get; }  // ← Added
    public ImmutableArray<SwitchCaseStatementSyntax> Cases { get; }
    public SwitchCaseStatementSyntax? DefaultCase { get; }
}
