using Minsk.CodeAnalysis.Binding.Statements;
using System.Collections.Immutable;
namespace Minsk.CodeAnalysis.Lowering.LoweredStatements
{
    internal sealed class DoWhileStatement
    {
        internal static BoundStatement Rewrite(Lowerer lowerer, BoundDoWhileStatement node)
        {
            // do
            //      <body>
            // while <condition>
            //
            // ----->
            //
            // continue:
            // <body>
            // check:
            // gotoTrue <condition> continue
            // end:
            //
            var continueLabel = lowerer.GenerateLabel();
            var checkLabel = lowerer.GenerateLabel();
            var endLabel = lowerer.GenerateLabel();

            var continueLabelStatement = new BoundLabelStatement(continueLabel);
            var checkLabelStatement = new BoundLabelStatement(checkLabel);
            var gotoTrue = new BoundConditionalGotoStatement(continueLabel, node.Condition);
            var endLabelStatement = new BoundLabelStatement(endLabel);

            var result = new BoundBlockStatement(ImmutableArray.Create(
                continueLabelStatement,
                node.Body,
                checkLabelStatement,
                gotoTrue,
                endLabelStatement
            ));
            return lowerer.RewriteStatement(result);
        }
    }
}