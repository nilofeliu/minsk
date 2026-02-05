using Minsk.CodeAnalysis.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Lowering
{
    internal sealed class Lowerer : BoundTreeRewrite
    {
        private Lowerer()
        {

        }

        public static BoundStatement Lower(BoundStatement statement)
        {
            var lowerer = new Lowerer();
            return lowerer.RewriteStatement(statement);
        }

    }
}
