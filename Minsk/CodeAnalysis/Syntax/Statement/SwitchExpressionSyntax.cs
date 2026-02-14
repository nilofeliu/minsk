using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax.Statement;

public sealed class SwitchStatementSyntax : StatementSyntax
{
    public SwitchStatementSyntax(
        SyntaxToken switchKeyword,
        ExpressionSyntax pattern,
        ImmutableArray<SwitchCaseStatementSyntax> cases,
        SwitchCaseStatementSyntax defaultCase, SyntaxToken endToken)
    {
        SwitchKeyword = switchKeyword;
        Pattern = pattern;
        Cases = cases;
        DefaultCase = defaultCase;
        EndToken = endToken;
    }

    public override SyntaxKind Kind => SyntaxKind.SwitchStatement;
    public SyntaxToken SwitchKeyword { get; }
    public ExpressionSyntax Pattern { get; }  // ← Added
    public ImmutableArray<SwitchCaseStatementSyntax>? Cases { get; }
    public SwitchCaseStatementSyntax? DefaultCase { get; }
    public SyntaxToken EndToken { get; }
}