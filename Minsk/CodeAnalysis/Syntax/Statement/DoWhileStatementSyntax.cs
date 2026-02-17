using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Statement
{
    internal class DoWhileStatementSyntax : StatementSyntax
    {

        public DoWhileStatementSyntax(SyntaxToken doKeyword, BlockStatementSyntax body, ExpressionSyntax condition)
        {
            DoKeyword = doKeyword;
            Body = body;
            Condition = condition;
        }

        public override SyntaxKind Kind => SyntaxKind.DoWhileStatement;
        public SyntaxToken DoKeyword { get; }
        public BlockStatementSyntax Body { get; }
        public ExpressionSyntax Condition { get; }
    }
}