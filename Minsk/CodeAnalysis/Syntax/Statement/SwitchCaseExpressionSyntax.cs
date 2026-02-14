using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax.Statement;

public sealed class SwitchCaseStatementSyntax : StatementSyntax
{
    public SwitchCaseStatementSyntax(
        SyntaxToken caseKeyword,
        ExpressionSyntax caseMatch,
        ImmutableArray<StatementSyntax> statement)
    {
        CaseKeyword = caseKeyword;
        Expression = caseMatch;
        Statement = statement;
    }

    public override SyntaxKind Kind => SyntaxKind.SwitchCaseKeyword;
    public SyntaxToken CaseKeyword { get; }
    public ExpressionSyntax Expression { get; }
    public ImmutableArray<StatementSyntax> Statement { get; }
}
