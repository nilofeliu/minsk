using Minsk.CodeAnalysis.Binding.Kind;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Binding.Statements;

internal sealed class  BoundBlockStatement : BoundStatement
{
    public BoundBlockStatement(ImmutableArray<BoundStatement> statements)
    {
        Statements = statements;
    }

    public ImmutableArray<BoundStatement> Statements { get; }

    public override BoundNodeKind Kind => BoundNodeKind.BlockStatement;
}
