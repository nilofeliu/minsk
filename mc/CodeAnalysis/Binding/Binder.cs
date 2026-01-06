using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis.Binding
{

    internal sealed class Binder
    {
        private readonly List<string> _diagnostic = new();

        public IEnumerable<string> Diagnostics => _diagnostic;

        public BoundExpression BindExpression(ExpressionSyntax syntax)
        {
            switch (syntax.Kind)
            {
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax);

                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);

                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Unexpected syntax node {syntax.Kind} <BINDER>");
                    Console.ResetColor();
                    throw new Exception($"Unexpected syntax node {syntax.Kind}");
            }
        }
        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
        {

            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }
        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
        {
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);
            if (boundOperator == null)
            {
                _diagnostic.Add($"Unary operator '{syntax.OperatorToken.Text}' is not defined for type {boundOperand.Type}.");
                return boundOperand;
            }
            return new BoundUnaryExpression(boundOperator, boundOperand);
        }

        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

            if (boundOperator == null)
            {
                _diagnostic.Add
                    ($"Binary operator '{syntax.OperatorToken.Text}' " +
                    $"is not defined for type {boundLeft.Type} " +
                    $"and {boundRight.Type}.");

                return boundLeft;
            }

            return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        }
        private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind kind, Type operandtype)
        {
            if (operandtype == typeof(int))
            {
                switch (kind)
                {
                    case SyntaxKind.PlusToken:
                        return BoundUnaryOperatorKind.Identity;
                    case SyntaxKind.MinusToken:
                        return BoundUnaryOperatorKind.Negation;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unexpected unary operator {kind} <BINDER>");
                        Console.ResetColor();
                        break;
                }
            }
            else if (operandtype == typeof(bool))
            {
                switch (kind)
                {
                    case SyntaxKind.BangToken:
                        return BoundUnaryOperatorKind.LogicalNegation;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unexpected unary operator {kind} <BINDER>");
                        Console.ResetColor();
                        break;
                }
            }

            return null;
        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(SyntaxKind kind, Type leftType, Type rightType)
        {
            if (leftType == typeof(int) && rightType == typeof(int))
            {
                switch (kind)
                {
                    case SyntaxKind.PlusToken:
                    return BoundBinaryOperatorKind.Addition;
                    case SyntaxKind.MinusToken:
                    return BoundBinaryOperatorKind.Subtraction;
                    case SyntaxKind.StarToken:
                    return BoundBinaryOperatorKind.Multiplication;
                    case SyntaxKind.SlashToken:
                    return BoundBinaryOperatorKind.Division;

                    default:
                    Console.WriteLine($"Unexpected binary operator {kind}");
                    return null;
                }
            }
            else if (leftType == typeof(bool) && rightType == typeof(bool))
            {
                switch (kind)
                {
                    case SyntaxKind.AmpersandAmpersandToken:
                    return BoundBinaryOperatorKind.LogicalAnd;
                    case SyntaxKind.PipePipeToken:
                    return BoundBinaryOperatorKind.LogicalOr;
                    default:
                    Console.WriteLine($"Unexpected binary operator {kind}");
                    return null;
                }
            }
            else
            {
                return null;

            }
        }
    }
}

