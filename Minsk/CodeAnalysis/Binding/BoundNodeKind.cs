namespace Minsk.CodeAnalysis.Binding
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
    }
}

