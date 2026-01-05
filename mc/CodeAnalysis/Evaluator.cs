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

        public object Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private object EvaluateExpression(BoundExpression node)
        {

            if (node is BoundLiteralExpression n)
                return EvaluateLiteralExpression(n);
            if (node is BoundUnaryExpression u)
                return EvaluateUnaryExpression(u);
            if (node is BoundBinaryExpression b)
                return EvaluateBinaryExpression(b);

            throw new Exception($"Unexpected node {node.Kind}");

        }
        private object EvaluateLiteralExpression(BoundLiteralExpression n)
        {
            return n.Value;
        }
        private object EvaluateUnaryExpression(BoundUnaryExpression u)
        {
            var operand = EvaluateExpression(u.Operand);

            switch (u.OperatorKind)
            {
                case BoundUnaryOperatorKind.Identity:
                    return (int)operand;
                case BoundUnaryOperatorKind.Negation:
                    return -(int)operand;
                case BoundUnaryOperatorKind.LogicalNegation:
                    return (bool)operand;
                default:
                    throw new Exception($"Unexpected unary operator {u.OperatorKind}");
            }
        }
        private object EvaluateBinaryExpression(BoundBinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);
         
            switch (b.OperatorKind)
            {
                case BoundBinaryOperatorKind.Addition:
                    return (int)left + (int)right;
                case BoundBinaryOperatorKind.Subtraction:
                    return (int)left - (int)right;
                case BoundBinaryOperatorKind.Multiplication:
                    return (int)left * (int)right;
                case BoundBinaryOperatorKind.Division:
                    return (int)left / (int)right;
                case BoundBinaryOperatorKind.LogicalAnd:
                    return (bool)left && (bool)right;
                case BoundBinaryOperatorKind.LogicalOr:
                    return (bool)left || (bool)right;
                default:
                    throw new Exception($"Unexpected binary operator {b.OperatorKind}");
            }
        }
    }
}
