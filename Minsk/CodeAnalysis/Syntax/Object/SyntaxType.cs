using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Object
{
    public sealed class SyntaxType
    {
        public SyntaxKind Kind { get; }
        public string Text { get; }
        public int Precedence { get; }

        public SyntaxType(SyntaxKind kind, string text, int precedence)
        {
            Kind = kind;
            Text = text;
            Precedence = precedence;
        }
    }
}
