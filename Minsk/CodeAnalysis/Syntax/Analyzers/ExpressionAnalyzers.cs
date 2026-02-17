using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Statement;
using static Minsk.CodeAnalysis.Syntax.Parser;

namespace Minsk.CodeAnalysis.Syntax.Analyzers;

internal class ExpressionAnalyzers
{

    private readonly PeekDelegate _peek;
    private readonly NextTokenDelegate _nextToken;
    private readonly MatchTokenDelegate _matchToken;

    private SyntaxToken Current => _peek(0);

    public ExpressionAnalyzers(
        PeekDelegate peek,
        NextTokenDelegate nextToken,
        MatchTokenDelegate matchToken
        )
    {
        _peek = peek;
        _nextToken = nextToken;
        _matchToken = matchToken;
    }

    internal ExpressionStatementSyntax ParseExpressionStatement()
    {
        var expression = ParseExpression();
        return new ExpressionStatementSyntax(expression);
    }

    internal ExpressionSyntax ParseExpression()
    {
        return ParseAssingmentExpression();
    }

    private ExpressionSyntax ParseAssingmentExpression()
    {

        if (_peek(0).Kind == SyntaxKind.IdentifierToken &&
            _peek(1).Kind == SyntaxKind.EqualsToken)
        {
            var identifierToken = _nextToken();
            var operatorToken = _nextToken();
            var right = ParseAssingmentExpression();

            return new AssignmentExpressionSyntax(identifierToken, operatorToken, right);
        }

        return ParseOperatorExpression();

    }

    private ExpressionSyntax ParseOperatorExpression(int parentPrecedence = 0)
    {
        ExpressionSyntax left;
        var unaryOperatorPrecedence = Current.Kind.GetUnaryOperatorPrecedence();
        if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
        {
            var operatorToken = _nextToken();
            var operand = ParseOperatorExpression(unaryOperatorPrecedence);
            left = new UnaryExpressionSyntax(operatorToken, operand);
        }
        else
        {
            left = ParsePrimaryExpression();
        }


        while (true)
        {
            var precedence = Current.Kind.GetBinaryOperatorPrecedence();
            if (precedence == 0 || precedence <= parentPrecedence)
                break;
            var operatorToken = _nextToken();
            var right = ParseOperatorExpression(precedence);
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }
        return left;
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        switch (Current.Kind)
        {
            case SyntaxKind.OpenParenthesisToken:
                {
                    return ParseParenthesizedExpression();
                }

            case SyntaxKind.TrueKeyword:
            case SyntaxKind.FalseKeyword:
                {
                    return ParseBooleanLiteral();
                }

            case SyntaxKind.NumberToken:
                {
                    return ParseNumberLiteral();
                }

            default:
                {
                    return ParseNameExpression();
                }
        }
    }

    private ExpressionSyntax ParseParenthesizedExpression()
    {
        var left = _nextToken();
        var expression = ParseAssingmentExpression();
        var right = _matchToken(SyntaxKind.CloseParenthesisToken);
        return new ParenthesizedExpressionSyntax(left, expression, right);
    }

    private ExpressionSyntax ParseBooleanLiteral()
    {
        var isTrue = Current.Kind == SyntaxKind.TrueKeyword;
        var keywordToken = isTrue ? _matchToken(SyntaxKind.TrueKeyword) : _matchToken(SyntaxKind.FalseKeyword);
        return new LiteralExpressionSyntax(keywordToken, isTrue);
    }

    private ExpressionSyntax ParseNumberLiteral()
    {
        var numberToken = _matchToken(SyntaxKind.NumberToken);
        return new LiteralExpressionSyntax(numberToken);
    }

    private ExpressionSyntax ParseNameExpression()
    {
        var identifierToken = _matchToken(SyntaxKind.IdentifierToken);
        return new NameExpressionSyntax(identifierToken);
    }

}

