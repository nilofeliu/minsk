using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Kind;
using Minsk.CodeAnalysis.Symbols;

namespace Minsk.CodeAnalysis.Binding.Statements;

internal sealed class BoundForeachStatement : BoundStatement
{
    public BoundForeachStatement(VariableSymbol variable, BoundExpression collection, BoundStatement body)
    {
        Variable = variable;
        Collection = collection;
        Body = body;
    }

    public override BoundNodeKind Kind => BoundNodeKind.ForeachStatement;
    public VariableSymbol Variable { get; }
    public BoundExpression Collection { get; }
    public BoundStatement Body { get; }
}