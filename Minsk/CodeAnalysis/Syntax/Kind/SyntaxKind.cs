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

        //Typed Variables
        VoidKeyword,
        CharKeyword,
        BoolKeyword,
        ByteKeyword,
        SByteKeyword,
        ShortKeyword,
        UShortKeyword,
        IntKeyword,
        UIntKeyword,
        LongKeyword,
        ULongKeyword,
        FloatKeyword,
        DoubleKeyword,
        DecimalKeyword,
        StringKeyword,
        ObjectKeyword,

        // Type .Net Frameworks Keywords
        SingleKeyword,
        Int8Keyword,
        Int16Keyword,
        Int32Keyword,
        Int64Keyword,
        UInt8Keyword,
        UInt16Keyword,
        UInt32Keyword,
        UInt64Keyword,

        //Punctuation Token
        ColonToken,
        //UnderscoreToken,
    }
}
