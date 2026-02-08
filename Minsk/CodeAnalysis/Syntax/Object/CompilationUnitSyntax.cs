using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Statement;

namespace Minsk.CodeAnalysis.Syntax.Object
{
    public sealed class CompilationUnitSyntax : SyntaxNode
    {
        public CompilationUnitSyntax(StatementSyntax statement, SyntaxToken endOfFileToken)
        {
            Statement = statement;
            EndOfFileToken = endOfFileToken;
        }

        public override SyntaxKind Kind => SyntaxKind.CompilationUnit;
        public StatementSyntax Statement { get; }
        public SyntaxToken EndOfFileToken { get; }

    }

}
