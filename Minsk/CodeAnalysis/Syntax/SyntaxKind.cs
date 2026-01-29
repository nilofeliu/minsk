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
        LessToken,
        LessOrEqualsToken,
        GreaterToken,
        GreaterOrEqualsToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,
        LetKeyword,
        VarKeyword,

        //Statements
        BlockStatement,
        ExpressionStatement,
        VariableDeclaration,

        // Expressions
        LiteralExpression,
        NameExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesisedExpression,
        AssignmentExpression,

    }
}
