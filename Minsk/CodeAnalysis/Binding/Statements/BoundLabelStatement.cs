using Minsk.CodeAnalysis.Binding.Kind;

namespace Minsk.CodeAnalysis.Binding.Statements
{
    internal sealed class BoundLabelStatement : BoundStatement
    {
        public BoundLabelStatement(LabelSymbol label)
        {
            Label = label;
        }
        public override BoundNodeKind Kind => BoundNodeKind.LabelStatement;

        public LabelSymbol Label { get; }
    }
}

