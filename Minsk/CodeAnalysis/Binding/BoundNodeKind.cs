namespace Minsk.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        None,
        Ref,
        Out,
        In,
        Params,
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        VariableExpression,
        AssignmentExpression,
    }
}

