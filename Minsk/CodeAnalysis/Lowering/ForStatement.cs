using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Objects;
using Minsk.CodeAnalysis.Binding.Statements;
using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Lowering
{
    internal static class ForStatement
    {
        internal static BoundStatement Rewrite(Lowerer lowerer, BoundForStatement node)
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

            return lowerer.RewriteStatement(result);
        }
    }
}