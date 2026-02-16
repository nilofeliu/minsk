namespace Minsk.CodeAnalysis.Symbols
{
    public sealed class VariableSymbol : ISymbol
    {
        public VariableSymbol(string name, bool isReadOnly, Type type)
        {
            Text = name;
            IsReadOnly = isReadOnly;
            Type = type;
        }
        public string Text { get; }
        public bool IsReadOnly { get; }
        public Type Type { get; }

        public override string ToString() => Text;
    }
}
