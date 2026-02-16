using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Symbols
{
    public sealed class SyntaxSymbol : ISymbol
    {
        public SyntaxKind Kind { get; }
        public string Text { get; }
        public int UnaryPrecedence { get; }
        public int BinaryPrecedence { get; }

        public SyntaxSymbol(SyntaxKind kind, string text, int binaryPrecedence = 0, int unaryPrecedence = 0)
        {
            Kind = kind;
            Text = text;
            UnaryPrecedence = unaryPrecedence;
            BinaryPrecedence = binaryPrecedence;
        }
    }


}
