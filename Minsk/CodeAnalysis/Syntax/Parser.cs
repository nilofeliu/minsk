using Minsk.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis.Text;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax
{
    internal sealed class Parser
    {
        private DiagnosticBag _diagnostic = new();
        private readonly SourceText _text;
        private readonly ImmutableArray<SyntaxToken> _tokens;
        private int _position;

        public DiagnosticBag Diagnostics => _diagnostic; 

        public Parser(SourceText text)
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.Lex();

                if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndOfFileToken);

            _text = text;
            _tokens = tokens.ToImmutableArray();
            _diagnostic.AddRange(lexer.Diagnostics);
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
            var expression = ParseExpression();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new CompilationUnitSyntax(expression, endOfFileToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseAssingmentExpression();
        }

        private ExpressionSyntax ParseAssingmentExpression()
        {

            if (Peek(0).Kind == SyntaxKind.IdentifierToken &&
                Peek(1).Kind == SyntaxKind.EqualsToken)
            {
                var identifierToken = NextToken();
                var operatorToken = NextToken();
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
                var operatorToken = NextToken();
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
                    var operatorToken = NextToken();
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
            var left = NextToken();
            var expression = ParseAssingmentExpression();
            var right = MatchToken(SyntaxKind.CloseParenthesisToken);
            return new ParenthesizedExpressionSyntax(left, expression, right);
        }

        private ExpressionSyntax ParseBooleanLiteral()
        {
            var isTrue = Current.Kind == SyntaxKind.TrueKeyword;
            var keywordToken = isTrue ? MatchToken(SyntaxKind.TrueKeyword) : MatchToken(SyntaxKind.FalseKeyword);
            return new LiteralExpressionSyntax(keywordToken, isTrue);
        }

        private ExpressionSyntax ParseNumberLiteral()
        {
            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }

        private ExpressionSyntax ParseNameExpression()
        {
            var identifierToken = MatchToken(SyntaxKind.IdentifierToken);
            return new NameExpressionSyntax(identifierToken);
        }


    }
}
