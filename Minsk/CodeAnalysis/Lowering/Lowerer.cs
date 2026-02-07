using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Lowering;

internal sealed class Lowerer : BoundTreeRewrite
{

    private int _labelCount;
    
    private Lowerer()
    {
    }

    private LabelSymbol GenerateLabel()
    {
        var name = $"Label{++_labelCount}";
        return new LabelSymbol(name);
    }

    public static BoundBlockStatement Lower(BoundStatement statement)
    {
        var lowerer = new Lowerer();
        var result = lowerer.RewriteStatement(statement);
        return Flatten(result);
    }

    private static BoundBlockStatement Flatten(BoundStatement statement)
    {
        var builder = ImmutableArray.CreateBuilder<BoundStatement>();
        var stack = new Stack<BoundStatement>();
        stack.Push(statement);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current is BoundBlockStatement block)
            {
                foreach (var s in block.Statements.Reverse())
                    stack.Push(s);
            }
            else
            {
                builder.Add(current);
            }
        }

        return new BoundBlockStatement(builder.ToImmutable());
    }

    //protected override BoundStatement RewriteIfStatement(BoundIfStatement node)
    //{
    //    if (node.ElseStatement == null)
    //    {
    //        // if <condition> 
    //        //      <then>
    //        // 
    //        // ---->
    //        // gotoifFalse <condition> end
    //        // <then>
    //        // end:

    //        var endLabel = GenerateLabel();
    //        var gotoFalse = new BoundConditionalGotoStatement(endLabel, node.Condition, true);
    //        var endLabelStatement = new BoundLabelStatement(endLabel);
    //        var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
    //            gotoFalse,
    //            node.ThenStatement,
    //            endLabelStatement)
    //        );

    //        return RewriteStatement(result);
    //    }
    //    else
    //    {
    //        // if <condition> 
    //        //      <then>
    //        // else
    //        //      <else>
    //        //
    //        // ---->
    //        //
    //        // gotoifFalse <condition> end
    //        // <then>
    //        // goto end
    //        // else:
    //        // <else>
    //        // end:

    //        var elseLabel = GenerateLabel();
    //        var endLabel = GenerateLabel();

    //        var gotoFalse = new BoundConditionalGotoStatement(elseLabel, node.Condition, true);
    //        var gotoEndStatement = new BoundGotoStatement(endLabel);
    //        var elseLabelStatement = new BoundLabelStatement(elseLabel);
    //        var endLabelStatement = new BoundLabelStatement(endLabel);
    //        var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
    //            gotoFalse,
    //            node.ThenStatement,
    //            gotoEndStatement,
    //            elseLabelStatement,
    //            node.ElseStatement,
    //            endLabelStatement                
    //            )
    //        );

    //        return RewriteStatement(result);        
    //    }
    //}

    protected override BoundStatement RewriteIfStatement(BoundIfStatement node)
    {
        if (node.ElseIfClauses.IsEmpty && node.ElseStatement == null)
        {
            // if <condition> 
            //      <then>
            // 
            // ---->
            // gotoifFalse <condition> end
            // <then>
            // end:
            var endLabel = GenerateLabel();
            var gotoFalse = new BoundConditionalGotoStatement(endLabel, node.Condition, true);
            var endLabelStatement = new BoundLabelStatement(endLabel);
            var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                gotoFalse,
                node.ThenStatement,
                endLabelStatement)
            );
            return RewriteStatement(result);
        }
        else
        {
            var statements = ImmutableArray.CreateBuilder<BoundStatement>();
            var endLabel = GenerateLabel();

            // Determine where the first if should jump when false
            LabelSymbol firstFalseLabel;
            if (!node.ElseIfClauses.IsEmpty)
                firstFalseLabel = GenerateLabel();
            else if (node.ElseStatement != null)
                firstFalseLabel = GenerateLabel();
            else
                firstFalseLabel = endLabel;

            // Main if condition
            statements.Add(new BoundConditionalGotoStatement(firstFalseLabel, node.Condition, true));
            statements.Add(node.ThenStatement);
            statements.Add(new BoundGotoStatement(endLabel));

            // Process each else-if
            if (!node.ElseIfClauses.IsEmpty)
            {
                statements.Add(new BoundLabelStatement(firstFalseLabel));

                for (int i = 0; i < node.ElseIfClauses.Length; i++)
                {
                    var elseIfClause = node.ElseIfClauses[i];

                    // Determine next label
                    LabelSymbol nextLabel;
                    if (i < node.ElseIfClauses.Length - 1)
                        nextLabel = GenerateLabel();
                    else if (node.ElseStatement != null)
                        nextLabel = GenerateLabel();
                    else
                        nextLabel = endLabel;

                    if (i > 0)
                        statements.Add(new BoundLabelStatement(GenerateLabel()));

                    statements.Add(new BoundConditionalGotoStatement(nextLabel, elseIfClause.Condition, true));
                    statements.Add(elseIfClause.Statement);
                    statements.Add(new BoundGotoStatement(endLabel));
                }
            }

            // Process else (if exists)
            if (node.ElseStatement != null)
            {
                var elseLabel = (!node.ElseIfClauses.IsEmpty) ? GenerateLabel() : firstFalseLabel;
                statements.Add(new BoundLabelStatement(elseLabel));
                statements.Add(node.ElseStatement);
            }

            statements.Add(new BoundLabelStatement(endLabel));

            var result = new BoundBlockStatement(statements.ToImmutable());
            return RewriteStatement(result);
        }
    }


    protected override BoundStatement RewriteWhileStatement(BoundWhileStatement node)
    {
        // while <condition>
        //      <body>
        //
        // ----->
        //
        // goto check
        // continue:
        // <body>
        // check:
        // gotoTrue <condition> continue
        // end:
        //

        var continueLabel = GenerateLabel();
        var checkLabel = GenerateLabel();
        var endLabel = GenerateLabel();

        var gotoCheck = new BoundGotoStatement(checkLabel);
        var continueLabelStatement = new BoundLabelStatement(continueLabel);
        var checkLabelStatement = new BoundLabelStatement(checkLabel);
        var gotoTrue = new BoundConditionalGotoStatement(continueLabel, node.Condition, false);
        var endLabelStatement = new BoundLabelStatement(endLabel);

        var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
            gotoCheck,
            continueLabelStatement,
            node.Body,
            checkLabelStatement,
            gotoTrue,
            endLabelStatement
        ));

        return RewriteStatement(result);
    }


    protected override BoundStatement RewriteForStatement(BoundForStatement node)
    {
        // for <var> = <lower> to <upper>
        //      <body>
        //
        // ---->
        //
        // {
        //      var <var> = <lower>
        //      while (<var> <= <upper>)
        //      {
        //          <body>
        //          <var> = <var> + 1
        //      }   
        // }

        var variableDeclaration = new BoundVariableDeclaration(node.Variable, node.LowerBound);
        var variableExpression = new BoundVariableExpression(node.Variable);
        var condition = new BoundBinaryExpression(
            variableExpression,
            BoundBinaryOperator.Bind(SyntaxKind.LessOrEqualsToken, typeof(int), typeof(int)),
            node.UpperBound
        );
        var increment = new BoundExpressionStatement(
            new BoundAssignmentExpression(
                node.Variable,
                new BoundBinaryExpression(
                        variableExpression,
                        BoundBinaryOperator.Bind(SyntaxKind.PlusToken, typeof(int), typeof(int)),
                        new BoundLiteralExpression(1)
                )
            )
        );
        var whileBody = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(node.Body, increment));
        var whileStatement = new BoundWhileStatement(condition, whileBody);
        var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(variableDeclaration, whileStatement));

        return RewriteStatement(result);
    }
    
}

