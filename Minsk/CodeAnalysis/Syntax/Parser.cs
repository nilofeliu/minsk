using Minsk.CodeAnalysis.Syntax.Analyzers;
using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Object;
using Minsk.CodeAnalysis.Text;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax;

internal sealed class Parser
{
    private DiagnosticBag _diagnostic = new();
    private readonly SourceText _text;
    private readonly ImmutableArray<SyntaxToken> _tokens;
    private int _position;

    private readonly ExpressionAnalyzers _expressionAnalyzer;
    private readonly StatementAnalyzers _statementAnalyzer;

    public DiagnosticBag Diagnostics => _diagnostic;

    public Parser(SourceText text)
    {
        List<SyntaxToken> tokens = new List<SyntaxToken>();

        var lexer = new Lexer(text);
        SyntaxToken token;
        SyntaxToken previousToken = null;

        do
        {
            token = lexer.Lex();
            if (SyntaxQuery.ContainsKeyword(token.Kind))
                token = ProcessToken(token, previousToken);

            if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                token.Kind != SyntaxKind.BadToken)
            {
                tokens.Add(token);
                previousToken = token;
            }

        } while (token.Kind != SyntaxKind.EndOfFileToken);

        _text = text;
        _tokens = tokens.ToImmutableArray();
        _diagnostic.AddRange(lexer.Diagnostics);

        PeekDelegate peekDelegate = Peek;
        CurrentDelegate currentDelegate = () => Current;
        NextTokenDelegate nextTokenDelegate = NextToken;
        MatchTokenDelegate matchTokenDelegate = MatchToken;

        _expressionAnalyzer = new ExpressionAnalyzers(Peek, NextToken, MatchToken);
        _statementAnalyzer = new StatementAnalyzers(Diagnostics, _expressionAnalyzer, Peek, NextToken, MatchToken);

    }

    internal SyntaxToken ProcessToken(SyntaxToken token, SyntaxToken? previousToken)
    {
        if (previousToken != null)
            if (previousToken.Kind == SyntaxKind.VarKeyword ||
            previousToken.Kind == SyntaxKind.LetKeyword)
            {
                var keywordKind = SyntaxKind.IdentifierToken;
                return new SyntaxToken(keywordKind, token.Position, token.Text, token.Value);
            }

        return token;

    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _tokens.Length)
            return _tokens[_tokens.Length - 1];

        return _tokens[index];
    }

    private SyntaxToken Current => Peek(0);

    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }

    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
            return NextToken();

        _diagnostic.ReportUnexpectedToken(Current.Span, Current.Kind, kind);
        return new SyntaxToken(kind, Current.Position, null, null);
    }

    public CompilationUnitSyntax ParseCompilationUnit()
    {
        var statement = _statementAnalyzer.ParseStatement();
        var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
        return new CompilationUnitSyntax(statement, endOfFileToken);
    }

    public delegate SyntaxToken PeekDelegate(int offset);
    public delegate SyntaxToken CurrentDelegate();
    public delegate SyntaxToken NextTokenDelegate();
    public delegate SyntaxToken MatchTokenDelegate(SyntaxKind kind);



}

