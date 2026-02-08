namespace Minsk.CodeAnalysis.Binding.Kind
{
    internal enum BoundBinaryOperatorKind
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,

        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,

        ExclusiveOr,
        LeftShift,
        RightShift,
        Equals,
        NotEquals,

        Less,
        LessOrEquals,
        Greater,
        GreaterOrEquals,

        LogicalAnd,
        LogicalOr,
    }
}

