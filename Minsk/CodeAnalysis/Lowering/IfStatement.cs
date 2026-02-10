using Minsk.CodeAnalysis.Binding.Statements;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Lowering
{


    internal static class IfStatement
    {

        internal static BoundStatement Rewrite(Lowerer lowerer, BoundIfStatement node)
        {
            if (node.ElseStatement == null)
            {
                // if <condition> 
                //      <then>
                // 
                // ---->
                // gotoifFalse <condition> end
                // <then>
                // end:

                var endLabel = lowerer.GenerateLabel();
                var gotoFalse = new BoundConditionalGotoStatement(endLabel, node.Condition, false);
                var endLabelStatement = new BoundLabelStatement(endLabel);
                var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                    gotoFalse,
                    node.ThenStatement,
                    endLabelStatement)
                );

                return lowerer.RewriteStatement(result);
            }
            else
            {
                // if <condition> 
                //      <then>
                // else
                //      <else>
                //
                // ---->
                //
                // gotoifFalse <condition> end
                // <then>
                // goto end
                // else:
                // <else>
                // end:

                var elseLabel = lowerer.GenerateLabel();
                var endLabel = lowerer.GenerateLabel();

                var gotoFalse = new BoundConditionalGotoStatement(elseLabel, node.Condition, false);
                var gotoEndStatement = new BoundGotoStatement(endLabel);
                var elseLabelStatement = new BoundLabelStatement(elseLabel);
                var endLabelStatement = new BoundLabelStatement(endLabel);
                var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                    gotoFalse,
                    node.ThenStatement,
                    gotoEndStatement,
                    elseLabelStatement,
                    node.ElseStatement,
                    endLabelStatement
                    )
                );

                return lowerer.RewriteStatement(result);
            }
        }

    }
}