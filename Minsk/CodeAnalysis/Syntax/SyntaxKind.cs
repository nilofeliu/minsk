namespace Minsk.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhiteSpaceToken,
        NumberToken,        
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        CloseBraceToken,
        OpenBraceToken,
        IdentifierToken,

        // Nodes
        CompilationUnit,

        // Operators
        EqualsToken,
        BangToken,
        AmpersandAmpersandToken,
        PipePipeToken,
        EqualsEqualsToken,
        BangEqualsToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,

        //Statements
        BlockStatement,
        ExpressionStatement,

        // Expressions
        LiteralExpression,
        NameExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesisedExpression,
        AssignmentExpression,
        VariableDeclaration,
    }
}
