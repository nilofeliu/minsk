using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Statement;

namespace Minsk.CodeAnalysis.Syntax.Object
{
    internal sealed class VariableDeclarationSyntax : StatementSyntax
    {
        public VariableDeclarationSyntax(SyntaxToken keyword, 
                                    SyntaxToken identifier,
                                    SyntaxToken equalsToken, 
                                    ExpressionSyntax initializer)
        {
            Keyword = keyword;
            Identifier = identifier;
            EqualsToken = equalsToken;
            Initializer = initializer;
        }

        public override SyntaxKind Kind => SyntaxKind.VariableDeclaration;
        public SyntaxToken Keyword { get; }
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public ExpressionSyntax Initializer { get; }

    }
}
