using Minsk.CodeAnalysis.Binding.Statements;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Lowering
{
    internal static class WhileStatement
    {
        internal static BoundStatement Rewrite(Lowerer lowerer, BoundWhileStatement node)
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

            var continueLabel = lowerer.GenerateLabel();
            var checkLabel = lowerer.GenerateLabel();
            var endLabel = lowerer.GenerateLabel();

            var gotoCheck = new BoundGotoStatement(checkLabel);
            var continueLabelStatement = new BoundLabelStatement(continueLabel);
            var checkLabelStatement = new BoundLabelStatement(checkLabel);
            var gotoTrue = new BoundConditionalGotoStatement(continueLabel, node.Condition);
            var endLabelStatement = new BoundLabelStatement(endLabel);

            var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                gotoCheck,
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