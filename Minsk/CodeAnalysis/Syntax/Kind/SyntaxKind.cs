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
        ElseIfClause,
        SwitchCaseClause,
        SwitchDefaultClause,

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
        ElseIfKeyword,
        ElseKeyword,
        ToKeyword,
        SwitchKeyword,
        SwitchCaseKeyword,
        SwitchDefaultKeyword,
        MatchKeyword,

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
        SwitchStatement,

        //Flow Control
        ContinueKeyword,
        BreakKeyword,
        EndKeyword,

        

        //Punctuation Token
        ColonToken,
        //UnderscoreToken,
    }
}
