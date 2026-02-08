using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Expression
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.ParenthesisedExpression;
        public SyntaxToken OpenParenthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParenthesisToken { get; }
        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }

    }
}
