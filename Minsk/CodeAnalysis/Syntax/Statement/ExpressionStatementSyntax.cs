using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Statement
{
    public sealed class ExpressionStatementSyntax : StatementSyntax
    {
        public ExpressionStatementSyntax(ExpressionSyntax expression)
        {
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;
        public ExpressionSyntax Expression { get; }

    }

}
