using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Expression;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Object;
using Minsk.CodeAnalysis.Syntax.Statement;
using Minsk.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Xml.Linq;

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

        private SyntaxToken Lookahead()
        { 
            return Peek(1);
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
            var statement = ParseStatement();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new CompilationUnitSyntax(statement, endOfFileToken);
        }

        private StatementSyntax ParseStatement()
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
                case SyntaxKind.ForKeyword:
                    return ParseForStatement();
                default:
                    return ParseExpressionStatement();
            }
        }

        private SyntaxKind ParseIdentifierKeyword()
        {
            return SyntaxKind.IdentifierToken;
        }

        private StatementSyntax ParseVariableDeclaration()
        {
            var expected = Current.Kind == SyntaxKind.LetKeyword ? SyntaxKind.LetKeyword : SyntaxKind.VarKeyword;
            var keyword = MatchToken(expected);
            var identifier = MatchToken(SyntaxKind.IdentifierToken);
            var equals = MatchToken(SyntaxKind.EqualsToken);
            var initializer = ParseExpression();

            if (SyntaxQuery.ContainsKeyword(identifier.Text))
                _diagnostic.ReportKeywordAsIdentifier(identifier.Span, identifier.Text);

            return new VariableDeclarationSyntax(keyword, identifier, equals, initializer);
        }



        private StatementSyntax ParseSwitchStatement()
        {
            SyntaxToken keyword;
            if (Current.Kind == SyntaxKind.MatchKeyword)
                keyword = MatchToken(SyntaxKind.MatchKeyword);
            else
                keyword = MatchToken(SyntaxKind.SwitchKeyword);
            
            var pattern = ParseExpression();
            MatchToken(SyntaxKind.ColonToken);
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
                    NextToken();
            }
            var cases = casesBuilder.ToImmutable();
              

            if (Current.Kind == SyntaxKind.SwitchDefaultKeyword && defaultCase == null)
                defaultCase = ParseSwitchCaseStatement();

            var endToken = MatchToken(SyntaxKind.EndKeyword);

            // var _cases = cases;  //ImmutableArray<SwitchCaseStatementSyntax>.Empty;

            var output = new SwitchStatementSyntax(keyword, pattern, cases, defaultCase, endToken);

            return output;

        }

        private SwitchCaseStatementSyntax ParseSwitchCaseStatement()
        {
            if (Current.Kind != SyntaxKind.SwitchCaseKeyword && Current.Kind != SyntaxKind.SwitchDefaultKeyword)
                return null;

            SyntaxToken keyword = NextToken();
            ExpressionSyntax caseExpression = null;
            if (keyword.Kind == SyntaxKind.SwitchCaseKeyword)
            {
                caseExpression = ParseExpression();
            }
            MatchToken(SyntaxKind.ColonToken);


            var statementBuilder = ImmutableArray.CreateBuilder<StatementSyntax>();
            while (Current.Kind != SyntaxKind.EndKeyword &&
                Current.Kind != SyntaxKind.SwitchDefaultKeyword &&
                Current.Kind != SyntaxKind.SwitchCaseKeyword &&
                Current.Kind != SyntaxKind.EndOfFileToken)  // ← See point 2
            {
                var starToken = Current;

                var statement = ParseStatement();
                statementBuilder.Add(statement);

                if (starToken == Current)
                    NextToken();
            }

            var statements = statementBuilder.ToImmutable();

            BlockStatementSyntax caseStatement = ParseScopedStatements(statements);

            return new SwitchCaseStatementSyntax(keyword, caseExpression, caseStatement);
        }


        private StatementSyntax ParseIfStatement()
        {
            var keyword = MatchToken(SyntaxKind.IfKeyword);
            var condition = ParseExpression();
            var colonToken = MatchToken(SyntaxKind.ColonToken);
            var casesBuilder = ImmutableArray.CreateBuilder<StatementSyntax>();

            while (Current.Kind != SyntaxKind.EndKeyword &&
                Current.Kind != SyntaxKind.ElseKeyword &&
                Current.Kind != SyntaxKind.ElseIfKeyword &&
                Current.Kind != SyntaxKind.EndOfFileToken)  // ← See point 2
            {
                var starToken = Current;
                // SwitchCaseStatementSyntax statement = null;

                    var statement = ParseStatement();
                casesBuilder.Add(statement);

                if (starToken == Current)
                    NextToken();
            }
            var statements = casesBuilder.ToImmutable();

            var elseClause = ParseElseClause();
            var endToken = MatchToken(SyntaxKind.EndKeyword);
                

            BlockStatementSyntax caseStatement = ParseScopedStatements(statements);

            return new IfStatementSyntax(keyword, condition, caseStatement, elseClause);
        }


        private ElseClauseSyntax ParseElseClause()        
        
        {
            if (Current.Kind != SyntaxKind.ElseKeyword && Current.Kind != SyntaxKind.ElseIfKeyword)
                return null;

            var keyword = NextToken();

            var casesBuilder = ImmutableArray.CreateBuilder<StatementSyntax>();

            if (keyword.Kind == SyntaxKind.ElseIfKeyword)
            {
                var condition = ParseExpression();
                var colonToken = MatchToken(SyntaxKind.ColonToken);

                while (Current.Kind != SyntaxKind.EndKeyword &&
                    Current.Kind != SyntaxKind.ElseKeyword &&
                    Current.Kind != SyntaxKind.ElseIfKeyword &&
                    Current.Kind != SyntaxKind.EndOfFileToken)
                {
                    var starToken = Current;
                    
                    var statement = ParseStatement();
                    casesBuilder.Add(statement);
                    
                    if (starToken == Current)
                        NextToken();
                }

                var statements = casesBuilder.ToImmutable();

                // RECURSIVE CALL: Parse more elseif/else clauses
                var elseClause = ParseElseClause();

                BlockStatementSyntax caseStatement = ParseScopedStatements(statements);

                
                var ifKeyword = new SyntaxToken(SyntaxKind.IfKeyword, keyword.Position, "if", null);
                return new ElseClauseSyntax(keyword,
                    new IfStatementSyntax(ifKeyword, condition, caseStatement, elseClause));
            }
            else
            {
                var colonToken = MatchToken(SyntaxKind.ColonToken);

                while (Current.Kind != SyntaxKind.EndKeyword &&
                    Current.Kind != SyntaxKind.ElseKeyword &&
                    Current.Kind != SyntaxKind.ElseIfKeyword &&
                    Current.Kind != SyntaxKind.EndOfFileToken)  
                {
                    var starToken = Current;
                    // SwitchCaseStatementSyntax statement = null;
                    
                    var statement = ParseStatement();
                    casesBuilder.Add(statement);
                    
                    if (starToken == Current)
                        NextToken();
                }

                var statements = casesBuilder.ToImmutable();
                BlockStatementSyntax caseStatement = ParseScopedStatements(statements);

                return new ElseClauseSyntax(keyword, caseStatement);
            }
        }

        private StatementSyntax ParseWhileStatement()
        {
            var keyword = MatchToken(SyntaxKind.WhileKeyword);
            var condition = ParseExpression();

            var colonToken = MatchToken(SyntaxKind.ColonToken);

            var casesBuilder = ImmutableArray.CreateBuilder<StatementSyntax>();

            while (Current.Kind != SyntaxKind.EndKeyword &&
                Current.Kind != SyntaxKind.EndOfFileToken)  // ← See point 2
            {
                var starToken = Current;
                // SwitchCaseStatementSyntax statement = null;
                
                var statement = ParseStatement();                
                casesBuilder.Add(statement);
                
                if (starToken == Current)
                    NextToken();
            }

            var endToken = MatchToken(SyntaxKind.EndKeyword);

            var statements = casesBuilder.ToImmutable();
            BlockStatementSyntax body = ParseScopedStatements(statements);


            return new WhileStatementSyntax(keyword, condition, body);
        }

        private StatementSyntax ParseForStatement()
        {
            var keyword = MatchToken(SyntaxKind.ForKeyword);
            var identifier = MatchToken(SyntaxKind.IdentifierToken);
            var equals = MatchToken(SyntaxKind.EqualsToken);
            var lowerBound = ParseExpression();
            var toKeword = MatchToken(SyntaxKind.ToKeyword);
            var upperBound = ParseExpression();
            var body = ParseStatement();
            
            return new ForStatementSyntax(keyword, identifier, equals, lowerBound, toKeword, upperBound, body);
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

            var openBraceToken = MatchToken(SyntaxKind.OpenBraceToken);
            

            while (Current.Kind != SyntaxKind.EndOfFileToken &&
                Current.Kind!= SyntaxKind.CloseBraceToken)
            {

                var startToken = Current;

                var statement = ParseStatement();
                statements.Add(statement);

                // if ParseStatement does not consume any token,
                // skip the current token and continue.
                // No error needs to be reporteed, because it has 
                // already been tried to parse the expression;

                if (Current == startToken) NextToken();
            }

            var closeBraceToken = MatchToken(SyntaxKind.CloseBraceToken);

            return new BlockStatementSyntax(openBraceToken,statements.ToImmutable(), closeBraceToken);

        }

        private ExpressionStatementSyntax ParseExpressionStatement()
        {
            var expression = ParseExpression();
            return new ExpressionStatementSyntax(expression);
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
