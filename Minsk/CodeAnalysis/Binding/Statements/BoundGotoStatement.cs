using Minsk.CodeAnalysis.Binding.Kind;

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

