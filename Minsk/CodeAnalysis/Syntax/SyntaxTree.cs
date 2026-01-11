using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class SyntaxTree
    {
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public SyntaxNode Root { get; }
        public SyntaxToken EndOfFileToken { get; }

        public SyntaxTree(ImmutableArray<Diagnostic> diagnostics, SyntaxNode root, SyntaxToken endOfFileToken)
        {
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }

        public static IEnumerable<SyntaxToken> ParseTokens(string text)
        {
            var lexer = new Lexer(text);
            while (true)
            {
                var token = lexer.Lex();
                if (token.Kind == SyntaxKind.EndOfFileToken)
                    break;

                yield return token;
            }
        }
    }
}
