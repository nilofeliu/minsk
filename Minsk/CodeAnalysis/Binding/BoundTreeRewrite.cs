using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Binding.Statements;
using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace Minsk.CodeAnalysis.Binding;

internal abstract class BoundTreeRewrite
{
    public virtual BoundStatement RewriteStatement(BoundStatement node)
    {
        switch (node.Kind)
        {

            case BoundNodeKind.BlockStatement:
                return RewriteBlockStatement((BoundBlockStatement)node);
            case BoundNodeKind.VariableDeclaration:
                return RewriteVariableDeclaration((BoundVariableDeclarationStatement)node);
            case BoundNodeKind.IfStatement:
                return RewriteIfStatement((BoundIfStatement)node);
            case BoundNodeKind.WhileStatement:
                return RewriteWhileStatement((BoundWhileStatement)node);
            case BoundNodeKind.ForStatement:
                return RewriteForStatement((BoundForStatement)node);
            case BoundNodeKind.SwitchStatement:
                return RewriteSwitchStatement((BoundSwitchStatement)node);
            case BoundNodeKind.LabelStatement:
                return RewriteLabelStatement((BoundLabelStatement)node);
            case BoundNodeKind.GotoStatement:
                return RewriteGotoStatement((BoundGotoStatement)node);
            case BoundNodeKind.ConditionalGotoStatement:
                return RewriteConditionalGotoStatement((BoundConditionalGotoStatement)node);

            case BoundNodeKind.ExpressionStatement:
                return RewriteExpressionStatement((BoundExpressionStatement)node);
            default:
                throw new Exception($"Unexpecte node : {node.Kind}");
        }
    }
    protected virtual BoundStatement RewriteBlockStatement(BoundBlockStatement node)
    {
        ImmutableArray<BoundStatement>.Builder builder = null;

        for (var i = 0; i < node.Statements.Length; i++)
        {
            BoundStatement oldStatement = node.Statements[i];
            var newStatement = RewriteStatement(oldStatement);

            if (newStatement != oldStatement)
            {
                if (builder == null)
                {
                    builder = ImmutableArray.CreateBuilder<BoundStatement>(node.Statements.Length);
                    for (var j = 0; j < i; j++)
                    {
                        builder.Add(node.Statements[j]);
                    }
                }
            }

            if (builder != null)
                builder.Add(newStatement);
        }

        if (builder == null)
            return node;

        return new BoundBlockStatement(builder.MoveToImmutable());
    }

    protected virtual BoundStatement RewriteVariableDeclaration(BoundVariableDeclarationStatement node)
    {
        var initializer = RewriteExpression(node.Initializer);
        if (initializer == node.Initializer)
            return node;

        return new BoundVariableDeclarationStatement(node.Variable, initializer);
    }

    protected virtual BoundStatement RewriteIfStatement(BoundIfStatement node)
    {
        var condition = RewriteExpression(node.Condition);
        var thenStatement = RewriteStatement(node.ThenStatement);
        var elseStatement = node.ElseStatement == null ? null :
            RewriteStatement(node.ElseStatement);

        if (condition == node.Condition && thenStatement == node.ThenStatement && elseStatement == node.ElseStatement)
            return node;
        
        return new BoundIfStatement(condition, thenStatement, elseStatement); 
    }

    protected virtual BoundStatement RewriteWhileStatement(BoundWhileStatement node)
    {
        var condition = RewriteExpression(node.Condition);
        var body = RewriteStatement(node.Body);

        if (condition == node.Condition && body == node.Body)
            return node;


        return new BoundWhileStatement(condition,body);
    }

    protected virtual BoundStatement RewriteForStatement(BoundForStatement node)
    {
        var lowerBound = RewriteExpression(node.LowerBound);
        var upperBound = RewriteExpression(node.UpperBound);
        var body = RewriteStatement(node.Body);
        if (lowerBound == node.LowerBound && upperBound == node.UpperBound && body == node.Body)
            return node;

        return new BoundForStatement(node.Variable, lowerBound, upperBound, body);
    }

    private BoundStatement RewriteSwitchStatement(BoundSwitchStatement node)
    {
        // switch (x) {
        //     case 1: stmt1;
        //     case 2: stmt2;
        //     default: stmtDefault;
        // }
        //
        // Becomes:
        //
        // if (x == 1)
        //     stmt1
        // else if (x == 2)
        //     stmt2
        // else
        //     stmtDefault

        BoundStatement? result = null;

        // Build from the end backwards (default first, then cases in reverse)
        if (node.DefaultCase != null)
        {
            result = node.DefaultCase.Body;
        }

        // Process cases in reverse order to build nested if-else chain
        if (node.Cases.HasValue)
        {
            for (int i = node.Cases.Value.Length - 1; i >= 0; i--)
            {
                var caseClause = node.Cases.Value[i];

                // Create equality check: switchValue == casePattern
                var condition = new BoundBinaryExpression(
                    node.Pattern,
                    BoundBinaryOperator.Bind(SyntaxKind.EqualsEqualsToken,
                                             node.Pattern.Type,
                                             caseClause.Pattern.Type),
                    caseClause.Pattern
                );

                var thenStatement = caseClause.Body;
                var elseStatement = result; // Previous result becomes else clause

                result = new BoundIfStatement(condition, thenStatement, elseStatement);
            }
        }

        // If no cases and no default, return empty statement
        return result ?? new BoundBlockStatement(ImmutableArray<BoundStatement>.Empty);
    }


    protected virtual BoundStatement RewriteLabelStatement(BoundLabelStatement node)
    {
        return node;
    }

    protected virtual BoundStatement RewriteGotoStatement(BoundGotoStatement node)
    {
        return node;
    }

    protected virtual BoundStatement RewriteConditionalGotoStatement(BoundConditionalGotoStatement node)
    {
        var condition = RewriteExpression(node.Condition);
        if (condition == node.Condition)
            return node;

        return new BoundConditionalGotoStatement(node.Label, condition, node.JumpIfTrue);
    }

    protected virtual BoundStatement RewriteExpressionStatement(BoundExpressionStatement node)
    {
        var expression = RewriteExpression(node.Expression);
        if (expression == node.Expression)
            return node;

        return new BoundExpressionStatement(expression);
    }

    public virtual BoundExpression RewriteExpression(BoundExpression node)
    {
        switch (node.Kind)
        {
            case BoundNodeKind.LiteralExpression:
                return RewriteLiteralExpression((BoundLiteralExpression)node);
            case BoundNodeKind.VariableExpression:
                return RewriteVariableExpression((BoundVariableExpression)node);
            case BoundNodeKind.AssignmentExpression:
                return RewriteAssignmentExpression((BoundAssignmentExpression)node);
            case BoundNodeKind.UnaryExpression:
                return RewriteUnaryExpression((BoundUnaryExpression)node);
            case BoundNodeKind.BinaryExpression:
                return RewriteBinaryExpression((BoundBinaryExpression)node);
            default:
                throw new Exception($"Unexpecte node : {node.Kind}");
        }
    }

    protected virtual BoundExpression RewriteBinaryExpression(BoundBinaryExpression node)
    {
        var left = RewriteExpression(node.Left);
        var right = RewriteExpression(node.Right);
        if (left == node.Left && right == node.Right)
            return node;
        return new BoundBinaryExpression(left, node.Op, right);
    }

    protected virtual BoundExpression RewriteUnaryExpression(BoundUnaryExpression node)
    {
        var operand = RewriteExpression(node.Operand);
        if (operand == node.Operand)
            return node;

        return new BoundUnaryExpression(node.Op, operand);
    }

    protected virtual BoundExpression RewriteAssignmentExpression(BoundAssignmentExpression node)
    {
        var expression = RewriteExpression(node.Expression);
        if (expression == node.Expression)
            return node;

        return new BoundAssignmentExpression(node.Variable, expression);
    }

    protected virtual BoundExpression RewriteVariableExpression(BoundVariableExpression node)
    {
        return node;
    }

    protected virtual BoundExpression RewriteLiteralExpression(BoundLiteralExpression node)
    {
        return node;
    }

}

