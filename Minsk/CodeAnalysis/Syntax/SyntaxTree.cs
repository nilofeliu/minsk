namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class SyntaxTree
    {
        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public SyntaxNode Root { get; }
        public SyntaxToken EndOfFileToken { get; }

        public SyntaxTree(IEnumerable<Diagnostic> diagnostics, SyntaxNode root, SyntaxToken endOfFileToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}
