using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Symbols
{
    public sealed class SyntaxSymbol
    {
        public SyntaxKind Kind { get; }
        public string Text { get; }
        public int Precedence { get; }

        public SyntaxSymbol(SyntaxKind kind, string text, int precedence)
        {
            Kind = kind;
            Text = text;
            Precedence = precedence;
        }
    }

    
}
