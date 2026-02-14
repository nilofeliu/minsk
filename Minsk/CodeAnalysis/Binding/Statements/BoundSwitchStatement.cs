using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Statement;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Binding.Statements;

internal sealed class BoundSwitchStatement : BoundStatement
{
    public BoundSwitchStatement(
        BoundExpression pattern,
        ImmutableArray<BoundSwitchCase>? cases,
        BoundSwitchCase? defaultCase)
    {
        Pattern = pattern;
        Cases = cases;
        DefaultCase = defaultCase;
    }

    public override BoundNodeKind Kind => BoundNodeKind.SwitchStatement;
    public BoundExpression Pattern { get; }  // ← Added
    public ImmutableArray<BoundSwitchCase>? Cases { get; }
    public BoundSwitchCase? DefaultCase { get; }
}

internal sealed class BoundSwitchCase : BoundNode
{
    public BoundSwitchCase(BoundExpression? pattern, BoundStatement body)
    {
        Pattern = pattern;
        Body = body;
    }

    public override BoundNodeKind Kind => BoundNodeKind.SwitchCase;

    public BoundExpression? Pattern { get; }  // Null for default case
    public BoundStatement Body { get; }
}
