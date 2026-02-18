using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Object;
using Minsk.CodeAnalysis.Syntax.Statement;
using System.Collections.Immutable;
using static Minsk.CodeAnalysis.Syntax.Parser;

namespace Minsk.CodeAnalysis.Syntax.Analyzers;

internal class StatementAnalyzers
{
    private readonly PeekDelegate _peek;
    private readonly NextTokenDelegate _nextToken;
    private readonly MatchTokenDelegate _matchToken;

    private readonly DiagnosticBag _diagnostic;
    private readonly ExpressionAnalyzers _expressionAnalyzer;

    private SyntaxToken Current => _peek(0);

    public StatementAnalyzers(
        DiagnosticBag diagnostic,
        ExpressionAnalyzers expressionAnalyzer,
        PeekDelegate peek,
        NextTokenDelegate nextToken,
        MatchTokenDelegate matchToken
        )
    {
        _diagnostic = diagnostic;
        _expressionAnalyzer = expressionAnalyzer;
        _peek = peek;
        _nextToken = nextToken;
        _matchToken = matchToken;
    }

    internal StatementSyntax ParseStatement()
    {
        switch (Current.Kind)
        {
            case SyntaxKind.OpenBraceToken:
                return ParseBlockStatement();
            case SyntaxKind.LetKeyword:
            case SyntaxKind.VarKeyword:
                return ParseVariableDeclaration();
            case SyntaxKind.IfKeyword:
                return ParseIfStatement();
            case SyntaxKind.SwitchKeyword:
                return ParseSwitchStatement();
            case SyntaxKind.MatchKeyword:
                return ParseSwitchStatement();
            case SyntaxKind.WhileKeyword:
                return ParseWhileStatement();
            case SyntaxKind.DoKeyword:
                return ParseDoWhileStatement();
            case SyntaxKind.ForKeyword:
                return ParseForStatement();
            default:
                return _expressionAnalyzer.ParseExpressionStatement();
        }
    }

    private StatementSyntax ParseVariableDeclaration()
    {
        var expected = Current.Kind == SyntaxKind.LetKeyword ? SyntaxKind.LetKeyword : SyntaxKind.VarKeyword;
        var keyword = _matchToken(expected);
        var identifier = _matchToken(SyntaxKind.IdentifierToken);
        var equals = _matchToken(SyntaxKind.EqualsToken);
        var initializer = _expressionAnalyzer.ParseExpression();

        if (SyntaxQuery.ContainsKeyword(identifier.Text))
            _diagnostic.ReportKeywordAsIdentifier(identifier.Span, identifier.Text);

        return new VariableDeclarationSyntax(keyword, identifier, equals, initializer);
    }

    private StatementSyntax ParseSwitchStatement()
    {
        SyntaxToken keyword;
        if (Current.Kind == SyntaxKind.MatchKeyword)
            keyword = _matchToken(SyntaxKind.MatchKeyword);
        else
            keyword = _matchToken(SyntaxKind.SwitchKeyword);

        var pattern = _expressionAnalyzer.ParseExpression();
        _matchToken(SyntaxKind.ColonToken);
        var casesBuilder = ImmutableArray.CreateBuilder<SwitchCaseStatementSyntax>();

        SwitchCaseStatementSyntax defaultCase = null;

        while (Current.Kind != SyntaxKind.EndKeyword &&
            Current.Kind != SyntaxKind.SwitchDefaultKeyword &&
            Current.Kind != SyntaxKind.EndOfFileToken)  // ← See point 2
        {
            var starToken = Current;
            // SwitchCaseStatementSyntax statement = null;

            if (Current.Kind == SyntaxKind.SwitchCaseKeyword)
            {
                var statement = ParseSwitchCaseStatement();

                if (statement.Expression is NameExpressionSyntax name &&
                    name.IdentifierToken.Text == "_")
                {
                    defaultCase = statement;
                    break;
                }

                casesBuilder.Add(statement);
            }
            if (starToken == Current)
                _nextToken();
        }
        var cases = casesBuilder.ToImmutable();


        if (Current.Kind == SyntaxKind.SwitchDefaultKeyword && defaultCase == null)
            defaultCase = ParseSwitchCaseStatement();

        var endToken = _matchToken(SyntaxKind.EndKeyword);

        var output = new SwitchStatementSyntax(keyword, pattern, cases, defaultCase, endToken);

        return output;

    }

    private SwitchCaseStatementSyntax ParseSwitchCaseStatement()
    {
        if (Current.Kind != SyntaxKind.SwitchCaseKeyword && Current.Kind != SyntaxKind.SwitchDefaultKeyword)
            return null;

        SyntaxToken keyword = _nextToken();
        ExpressionSyntax caseExpression = null;
        if (keyword.Kind == SyntaxKind.SwitchCaseKeyword)
        {
            caseExpression = _expressionAnalyzer.ParseExpression();
        }

        _matchToken(SyntaxKind.ColonToken);

        var caseStatement = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken,
            SyntaxKind.SwitchDefaultKeyword, SyntaxKind.SwitchCaseKeyword);

        return new SwitchCaseStatementSyntax(keyword, caseExpression, caseStatement);
    }


    private StatementSyntax ParseIfStatement()
    {
        var keyword = _matchToken(SyntaxKind.IfKeyword);
        var condition = _expressionAnalyzer.ParseExpression();
        var colonToken = _matchToken(SyntaxKind.ColonToken);

        var caseStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
            SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);

        var elseClause = ParseElseClause();
        var endToken = _matchToken(SyntaxKind.EndKeyword);



        return new IfStatementSyntax(keyword, condition, caseStatement, elseClause);
    }


    private ElseClauseSyntax ParseElseClause()

    {
        if (Current.Kind != SyntaxKind.ElseKeyword && Current.Kind != SyntaxKind.ElseIfKeyword)
            return null;

        var keyword = _nextToken();

        if (keyword.Kind == SyntaxKind.ElseIfKeyword)
        {
            var condition = _expressionAnalyzer.ParseExpression();
            var colonToken = _matchToken(SyntaxKind.ColonToken);

            // RECURSIVE CALL: Parse more elseif/else clauses
            var caseStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);


            var elseClause = ParseElseClause();

            var ifKeyword = new SyntaxToken(SyntaxKind.IfKeyword, keyword.Position, "if", null);
            return new ElseClauseSyntax(keyword,
                new IfStatementSyntax(ifKeyword, condition, caseStatement, elseClause));
        }
        else
        {
            var colonToken = _matchToken(SyntaxKind.ColonToken);

            var caseStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);

            return new ElseClauseSyntax(keyword, caseStatement);
        }
    }

    private StatementSyntax ParseWhileStatement()
    {
        var keyword = _matchToken(SyntaxKind.WhileKeyword);
        var condition = _expressionAnalyzer.ParseExpression();

        var colonToken = _matchToken(SyntaxKind.ColonToken);

        var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);

        var endToken = _matchToken(SyntaxKind.EndKeyword);

        return new WhileStatementSyntax(keyword, condition, body);
    }

    private StatementSyntax ParseDoWhileStatement()
    {
        var keyword = _matchToken(SyntaxKind.DoKeyword);
        var colonToken = _matchToken(SyntaxKind.ColonToken);

        var body = ParseMultiStatements(SyntaxKind.WhileKeyword);

        var whileToken = _matchToken(SyntaxKind.WhileKeyword);
        var condition = _expressionAnalyzer.ParseExpression();
               

        return new DoWhileStatementSyntax(keyword, body, condition);
    }

    private StatementSyntax ParseForStatement()
    {
        var keyword = _matchToken(SyntaxKind.ForKeyword);
        var identifier = _matchToken(SyntaxKind.IdentifierToken);
        var equals = _matchToken(SyntaxKind.EqualsToken);
        var lowerBound = _expressionAnalyzer.ParseExpression();
        var toKeword = _matchToken(SyntaxKind.ToKeyword);
        var upperBound = _expressionAnalyzer.ParseExpression();
        var colonToken = _matchToken(SyntaxKind.ColonToken);
        var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
        //var body = ParseStatement();
        var endToken = _matchToken(SyntaxKind.EndKeyword);

        return new ForStatementSyntax(keyword, identifier, equals, lowerBound, toKeword, upperBound, body);
    }

    private BlockStatementSyntax ParseMultiStatements(params SyntaxKind[] terminatingKinds)
    {
        if (Current.Kind == SyntaxKind.OpenBraceToken)
        {
            return ParseBlockStatement();
        }

        var builder = ImmutableArray.CreateBuilder<StatementSyntax>();

        while (!terminatingKinds.Contains(Current.Kind) &&
               Current.Kind != SyntaxKind.EndOfFileToken)
        {
            var startToken = Current;
            var statement = ParseStatement();
            builder.Add(statement);

            if (startToken == Current)
                _nextToken();
        }

        var statements = builder.ToImmutable();
        return ParseScopedStatements(statements);
    }

    private static BlockStatementSyntax ParseScopedStatements(ImmutableArray<StatementSyntax> statements)
    {
        return new BlockStatementSyntax(
            new SyntaxToken(SyntaxKind.OpenBraceToken, 0, "", null),
            statements,
            new SyntaxToken(SyntaxKind.CloseBraceToken, 0, "", null));
    }

    private BlockStatementSyntax ParseBlockStatement()
    {
        var statements = ImmutableArray.CreateBuilder<StatementSyntax>();

        var openBraceToken = _matchToken(SyntaxKind.OpenBraceToken);


        while (Current.Kind != SyntaxKind.EndOfFileToken &&
            Current.Kind != SyntaxKind.CloseBraceToken)
        {

            var startToken = Current;

            var statement = ParseStatement();
            statements.Add(statement);

            // if ParseStatement does not consume any token,
            // skip the current token and continue.
            // No error needs to be reporteed, because it has 
            // already been tried to parse the expression;

            if (Current == startToken) _nextToken();
        }

        var closeBraceToken = _matchToken(SyntaxKind.CloseBraceToken);

        return new BlockStatementSyntax(openBraceToken, statements.ToImmutable(), closeBraceToken);

    }

}

