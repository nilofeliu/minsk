using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Symbols;

namespace Minsk.CodeAnalysis.Binding.Statements
{
    internal sealed class BoundGotoStatement : BoundStatement
    {
        public BoundGotoStatement(LabelSymbol label)
        {
            Label = label;
        }

        public override BoundNodeKind Kind => BoundNodeKind.GotoStatement;

        public LabelSymbol Label { get; }
    }
}

