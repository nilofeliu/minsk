using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax.Statement
{
    public sealed class BlockStatementSyntax : StatementSyntax
    {
        public BlockStatementSyntax(SyntaxToken openBraceToken,
            ImmutableArray<StatementSyntax> statements,
            SyntaxToken closedBraceToken)
        {
            OpenBraceToken = openBraceToken;
            Statements = statements;
            ClosedBraceToken = closedBraceToken;
        }
        public override SyntaxKind Kind => SyntaxKind.BlockStatement;
        public SyntaxToken OpenBraceToken { get; }
        public ImmutableArray<StatementSyntax> Statements { get; }
        public SyntaxToken ClosedBraceToken { get; }
    }

}
