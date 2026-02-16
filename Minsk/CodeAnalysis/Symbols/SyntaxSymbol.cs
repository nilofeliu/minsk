using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Symbols
{
    public sealed class SyntaxSymbol
    {
        public SyntaxKind Kind { get; }
        public string Text { get; }
        public int BinaryPrecedence { get; }
        public int UnaryPrecedence { get; }

        public SyntaxSymbol(SyntaxKind kind, string text, int binaryPrecedence = 0, int unaryPrecedence = 0)
        {
            Kind = kind;
            Text = text;
            BinaryPrecedence = binaryPrecedence;
            UnaryPrecedence = unaryPrecedence;
        }
    }

    
}
