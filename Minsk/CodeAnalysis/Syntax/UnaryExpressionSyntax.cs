namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Operand { get; }

        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

    }
}
