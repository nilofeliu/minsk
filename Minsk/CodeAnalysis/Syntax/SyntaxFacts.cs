
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax
{
    public static class SyntaxFacts
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.BangToken:
                case SyntaxKind.TildeToken:
                    return 6;   

                default:
                return 0;
            }
            ;
        }

        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 5;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.BangEqualsToken:
                case SyntaxKind.GreaterOrEqualsToken:
                case SyntaxKind.GreaterToken:
                case SyntaxKind.LessOrEqualsToken:
                case SyntaxKind.LessToken:
                    return 3;

                case SyntaxKind.AmpersandAmpersandToken:
                case SyntaxKind.AmpersandToken:
                    return 2;

                case SyntaxKind.PipeToken:
                case SyntaxKind.PipePipeToken:
                case SyntaxKind.HatToken:
                    return 1;

                default:
                    return 0;
            };
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {

                case "false":
                    return SyntaxKind.FalseKeyword;
                case "for":
                    return SyntaxKind.ForKeyword;
                case "if":
                    return SyntaxKind.IfKeyword;
                case "else":
                    return SyntaxKind.ElseKeyword;
                case "let":
                    return SyntaxKind.LetKeyword;
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "var":
                    return SyntaxKind.VarKeyword;
                case "while":
                    return SyntaxKind.WhileKeyword;
                case "to":
                    return SyntaxKind.ToKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }

        public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));
            foreach (var kind in kinds)
            {
                if (GetUnaryOperatorPrecedence(kind) > 0)
                    yield return kind;
            }
        }
        public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));
            foreach (var kind in kinds)
            {
                if (GetBinaryOperatorPrecedence(kind) > 0)
                    yield return kind;
            }
        }
        public static string? GetText(SyntaxKind kind)
        {
            return kind switch
            {
                SyntaxKind.PlusToken => "+",
                SyntaxKind.MinusToken => "-",
                SyntaxKind.StarToken => "*",
                SyntaxKind.SlashToken => "/",

                SyntaxKind.OpenParenthesisToken => "(",
                SyntaxKind.CloseParenthesisToken => ")",
                SyntaxKind.OpenBraceToken => "{",
                SyntaxKind.CloseBraceToken => "}",

                SyntaxKind.EqualsToken => "=",
                SyntaxKind.EqualsEqualsToken => "==",
                SyntaxKind.GreaterToken => ">",
                SyntaxKind.GreaterOrEqualsToken => ">=",
                SyntaxKind.LessToken => "<",
                SyntaxKind.LessOrEqualsToken => "<=",
                SyntaxKind.BangToken => "!",
                SyntaxKind.BangEqualsToken => "!=",
                SyntaxKind.AmpersandAmpersandToken => "&&",
                SyntaxKind.AmpersandToken => "&",
                SyntaxKind.PipePipeToken => "||",
                SyntaxKind.PipeToken => "|",
                SyntaxKind.TildeToken => "~",
                SyntaxKind.HatToken => "^",

                SyntaxKind.TrueKeyword => "true",
                SyntaxKind.FalseKeyword => "false",
                SyntaxKind.LetKeyword => "let",
                SyntaxKind.VarKeyword => "var",
                SyntaxKind.IfKeyword => "if",
                SyntaxKind.ElseKeyword => "else",
                SyntaxKind.WhileKeyword => "while",
                SyntaxKind.ForKeyword => "for",
                SyntaxKind.ToKeyword => "to",
                _ => null,
            };
        }
    }
}
