namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class CompilationUnitSyntax : SyntaxNode
    {
        public CompilationUnitSyntax(ExpressionSyntax expression, SyntaxToken endOfFile)
        {
            Expression = expression;
            EndOfFile = endOfFile;
        }

        public override SyntaxKind Kind => SyntaxKind.CompilationUnit;
        public ExpressionSyntax Expression { get; }
        public SyntaxToken EndOfFile { get; }

    }

}
