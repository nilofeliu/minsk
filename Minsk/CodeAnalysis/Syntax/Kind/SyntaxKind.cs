namespace Minsk.CodeAnalysis.Syntax.Kind
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhiteSpaceToken,
        NumberToken,      
        
        // Math Operators Tokens
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,

        // Scope Tokens
        OpenParenthesisToken,
        CloseParenthesisToken,
        CloseBraceToken,
        OpenBraceToken,

        // Other Tokens
        IdentifierToken,

        //Punctuation Token
        ColonToken,
        
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

        // Boolean Keywords
        TrueKeyword,
        FalseKeyword,

        // Assignment Keywords
        LetKeyword,
        VarKeyword,

        // Control Keywords
        DoKeyword,
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

        //Flow Control
        ContinueKeyword,
        BreakKeyword,
        EndKeyword,

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
        DoWhileStatement,
        VariableDeclaration,
        SwitchStatement,

        //Primitive Keywords
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
        VoidKeyword,

        //ObjectKeyword,

    }
}
