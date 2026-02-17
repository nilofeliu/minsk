namespace Minsk.CodeAnalysis.Binding.Kind
{
    internal enum BoundNodeKind
    {
        None,
        Ref,
        Out,
        In,
        Params,

        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        VariableExpression,
        AssignmentExpression,

        //Statements
        BlockStatement,
        ExpressionStatement,
        VariableDeclaration,
        IfStatement,
        WhileStatement,
        ForStatement,
        GotoStatement,
        LabelStatement,
        ConditionalGotoStatement,
        SwitchStatement,
        SwitchCase,
        ForeachStatement,
        DoWhileStatement,
    }
}

