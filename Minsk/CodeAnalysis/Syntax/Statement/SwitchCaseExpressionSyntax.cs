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
        ExpressionSyntax? caseMatch,
        StatementSyntax? body)
    {
        CaseKeyword = caseKeyword;
        Expression = caseMatch;
        Body = body;

    }

    public override SyntaxKind Kind =>
     CaseKeyword.Kind == SyntaxKind.SwitchCaseKeyword
         ? SyntaxKind.SwitchCaseClause
         : SyntaxKind.SwitchDefaultClause;
    public SyntaxToken CaseKeyword { get; }
    public ExpressionSyntax? Expression { get; }
    public StatementSyntax? Body { get; }
}
