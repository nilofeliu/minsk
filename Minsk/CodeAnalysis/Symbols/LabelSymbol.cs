namespace Minsk.CodeAnalysis.Symbols
{
    public sealed class LabelSymbol : ISymbol
    {
        public LabelSymbol(string name)
        {
            Text = name;
        }

        public string Text { get; }

        public override string ToString() => Text;
    }
}
