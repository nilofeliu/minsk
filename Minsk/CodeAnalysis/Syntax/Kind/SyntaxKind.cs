namespace Minsk.CodeAnalysis.Syntax.Kind
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
        ElseClause,

        // Operators
        AmpersandToken,
        AmpersandAmpersandToken,
        BangToken,
        BangEqualsToken,
        EqualsToken,
        EqualsEqualsToken,
        LessToken,
        LessOrEqualsToken,
        GreaterToken,
        GreaterOrEqualsToken,
        HatToken,
        PipeToken,
        PipePipeToken,
        TildeToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,
        LetKeyword,
        VarKeyword,
        ForKeyword,
        WhileKeyword,
        IfKeyword,
        ElseKeyword,
        ToKeyword,

        // Expressions
        AssignmentExpression,
        BinaryExpression,
        LiteralExpression,
        NameExpression,
        ParenthesisedExpression,
        UnaryExpression,

        //Statements
        BlockStatement,
        ExpressionStatement,
        ForStatement,
        IfStatement,
        WhileStatement,
        VariableDeclaration,
    }
}
