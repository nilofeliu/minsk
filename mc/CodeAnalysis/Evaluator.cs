using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis
{
    internal class Evaluator
    {

        private readonly BoundExpression _root;

        public Evaluator(BoundExpression root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(BoundExpression node)
        {
            switch (node)
            {
                case BoundLiteralExpression n:
                    return EvaluateLiteralExpression(n);
                case BoundUnaryExpression u:
                    return EvaluateUnaryExpression(u);
                case BoundBinaryExpression b:
                    return EvaluateBinaryExpression(b);
                default:
                    throw new Exception($"Unexpected node {node.Kind}");

            }
        }
        private int EvaluateLiteralExpression(BoundLiteralExpression node)
        {
            return (int)node.Value;
        }
        private int EvaluateUnaryExpression(BoundUnaryExpression node)
        {
            var operand = EvaluateExpression(node.Operand);
            switch (node.OperatorKind)
            {
                case BoundUnaryOperatorKind.Identity:
                    return operand;
                case BoundUnaryOperatorKind.Negation:
                    return -operand;
                default:
                    throw new Exception($"Unexpected unary operator {node.OperatorKind}");
            }
        }
        private int EvaluateBinaryExpression(BoundBinaryExpression node)
        {
            var left = EvaluateExpression(node.Left);
            var right = EvaluateExpression(node.Right);
            switch (node.Operatorkind)
            {
                case BoundBinaryOperatorKind.Addition:
                    return left + right;
                case BoundBinaryOperatorKind.Subtraction:
                    return left - right;
                case BoundBinaryOperatorKind.Multiplication:
                    return left * right;
                case BoundBinaryOperatorKind.Division:
                    return left / right;
                default:
                    throw new Exception($"Unexpected binary operator {node.Operatorkind}");
            }
        }
    }
}
