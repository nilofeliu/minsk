using Minsk.CodeAnalysis.Syntax;
using static System.Net.Mime.MediaTypeNames;

namespace Minsk.Tests.CodeAnalysis.Syntax;

public class LexerTest
{
    [Theory]
    [MemberData(nameof(GetTokensData))]
    public void Lexer_Lexes_Token(SyntaxKind kind, string text)
    {
        var tokens = SyntaxTree.ParseTokens(text);

        var token = Assert.Single(tokens);
        Assert.Equal(kind, token.Kind);
        Assert.Equal(text, token.Text);
    }
    [Theory]
    [MemberData(nameof(GetTokensPairsData))]
    public void Lexer_Lexes_TokenPairs(SyntaxKind t1Kind, string t1Text,
                                       SyntaxKind t2Kind, string t2Text)
    {
        var text = t1Text + t2Text;
        var tokens = SyntaxTree.ParseTokens(text).ToArray();

        Assert.Equal(2, tokens.Length);
        Assert.Equal(tokens[0].Kind, t1Kind);
        Assert.Equal(tokens[0].Text, t1Text);
        Assert.Equal(tokens[1].Kind, t2Kind);
        Assert.Equal(tokens[1].Text, t2Text);
    }

    [Theory]
    [MemberData(nameof(GetTokensPairsWithSeparatorData))]
    public void Lexer_Lexes_TokenPairs_WithSeparator(SyntaxKind t1Kind, string t1Text,
        SyntaxKind separatorKind, string separatorText, 
        SyntaxKind t2Kind, string t2Text)
    {
        var text = t1Text + separatorText + t2Text;
        var tokens = SyntaxTree.ParseTokens(text).ToArray();

        Assert.Equal(3, tokens.Length);
        Assert.Equal(tokens[0].Kind, t1Kind);
        Assert.Equal(tokens[0].Text, t1Text);
        Assert.Equal(tokens[1].Kind, separatorKind);
        Assert.Equal(tokens[1].Text, separatorText);
        Assert.Equal(tokens[2].Kind, t2Kind);
        Assert.Equal(tokens[2].Text, t2Text);
    }

    public static IEnumerable<object?[]> GetTokensData()
    {
        var test = new LexerTest();
        foreach (var t in GetTokens().Concat(GetSeparator()))
        {
            yield return new object?[] { t.kind, t.text };
        }
    }

    public static IEnumerable<object?[]> GetTokensPairsData()
    {
        var test = new LexerTest();
        foreach (var t in GetTokensPairs())
        {
            yield return new object?[] { t.t1kind, t.t1text, t.t2kind, t.t2text };
        }
    }

    public static IEnumerable<object?[]> GetTokensPairsWithSeparatorData()
    {
        var test = new LexerTest();
        foreach (var t in GetTokensPairsWithSeparator())
        {
            yield return new object?[] { t.t1kind, t.t1text,t.separatorKind, t.separatorText, t.t2kind, t.t2text };
        }
    }

    private static IEnumerable<(SyntaxKind kind, string text)> GetTokens()
    {
        return new[]
        {
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.StarToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.OpenParenthesisToken, "("),
            (SyntaxKind.CloseParenthesisToken, ")"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.BangToken, "!"),
            (SyntaxKind.AmpersandAmpersandToken, "&&"),
            (SyntaxKind.PipePipeToken, "||"),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.BangEqualsToken, "!="),

            (SyntaxKind.IdentifierToken, "a"),
            (SyntaxKind.IdentifierToken, "abc"),
            (SyntaxKind.TrueKeyword, "true"),
            (SyntaxKind.FalseKeyword, "false"),

            (SyntaxKind.NumberToken, "1"),
            (SyntaxKind.NumberToken, "123"),

        };
    }
    private static IEnumerable<(SyntaxKind t1kind, string t1text, SyntaxKind t2kind, string t2text)> GetTokensPairs()
    {
        foreach (var t1 in GetTokens())
        {
            foreach (var t2 in GetTokens())
            {
                if (!RequiresSeparator(t1.kind, t2.kind))
                    yield return (t1.kind, t1.text, t2.kind, t2.text);
            }
        }
    }

    private static IEnumerable<(SyntaxKind t1kind, string t1text,
        SyntaxKind separatorKind, string separatorText, SyntaxKind t2kind, string t2text)> GetTokensPairsWithSeparator()
    {
        foreach (var t1 in GetTokens())
        {
            foreach (var t2 in GetTokens())
            {
                foreach (var s in GetSeparator())
                    yield return (t1.kind, t1.text, s.kind, s.text, t2.kind, t2.text);
            }
        }
    }

    private static bool RequiresSeparator(SyntaxKind t1, SyntaxKind t2)
    {

        var t1keywords = t1.ToString().EndsWith("Keyword");
        var t2keywords = t2.ToString().EndsWith("Keyword");

        if (t1keywords && t2keywords)
            return true;
        if (t1keywords && t2 == SyntaxKind.IdentifierToken)
            return true;
        if (t1 == SyntaxKind.IdentifierToken && t2keywords)
            return true;

        if (t1 == SyntaxKind.IdentifierToken && t2 == SyntaxKind.IdentifierToken)
            return true;

        if (t1 == SyntaxKind.NumberToken && t2 == SyntaxKind.NumberToken)
            return true;

        if (t1 == SyntaxKind.BangToken && t2 == SyntaxKind.EqualsToken)
            return true;
        if (t1 == SyntaxKind.BangToken && t2 == SyntaxKind.EqualsEqualsToken)
            return true;

        if (t1 == SyntaxKind.EqualsToken && t2 == SyntaxKind.EqualsToken)
            return true;
        if (t1 == SyntaxKind.EqualsToken && t2 == SyntaxKind.EqualsEqualsToken)
            return true;

        if (t1 == SyntaxKind.AmpersandAmpersandToken && t2 == SyntaxKind.AmpersandAmpersandToken)
            return true;
        if (t1 == SyntaxKind.PipePipeToken && t2 == SyntaxKind.PipePipeToken)
            return true;
                       
        if (t1 == SyntaxKind.WhiteSpaceToken && t2 == SyntaxKind.WhiteSpaceToken)
            return true;
        return false;
    }

    private static IEnumerable<(SyntaxKind kind, string text)> GetSeparator()
    {
        return new[]
        {
            (SyntaxKind.WhiteSpaceToken, " "),
            (SyntaxKind.WhiteSpaceToken, "  "),
            (SyntaxKind.WhiteSpaceToken, "\r"),
            (SyntaxKind.WhiteSpaceToken, "\n"),
            (SyntaxKind.WhiteSpaceToken, "\r\n"),
        };

    }
}
