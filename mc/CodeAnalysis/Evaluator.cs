using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis
{
    public class Evaluator
    {

        private readonly ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is LiteralExpressionSyntax n)
            {
                return (int)n.LiteralToken.Value;
            }
            else if (node is UnaryExpressionSyntax u)
            {
                var operand = EvaluateExpression(u.Operand);
                switch (u.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                        return operand;
                    case SyntaxKind.MinusToken:
                        return -operand;
                    default:
                        throw new Exception($"Unexpected unary operator {u.OperatorToken.Kind}");
                }
            }
            else if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                switch (b.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                    return left + right;
                    case SyntaxKind.MinusToken:
                    return left - right;
                    case SyntaxKind.StarToken:
                    return left * right;
                    case SyntaxKind.SlashToken:
                    return left / right;
                    default:
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }
            else if (node is ParenthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }
            else
            {
                throw new Exception($"Unexpected syntax node {node.Kind}");
            }
        }
    }
}
