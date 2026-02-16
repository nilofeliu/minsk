using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Core;

internal static class SyntaxDefinitions
    {
    // Load Syntax Token Delimiters

    public static void TryAddSymbol(List<SyntaxSymbol> list, SyntaxSymbol symbol)
    {
        if (!list.Any(s => s.Kind == symbol.Kind))
        {
            list.Add(symbol);
        }
    }
    
    internal static List<SyntaxSymbol> LoadPunctuationDelimiters()
    {
        var punctuationTokens = new List<SyntaxSymbol>();
        TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.ColonToken, ":"));

        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.CommaToken, ",",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.SemicolonToken, ";",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.UnderscoreToken, "_",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.DotToken, ".",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.QuestionToken, "?",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.ExclamationToken, "!",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.AtToken, "@",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.HashToken, "#",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.DollarToken, "$",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.PercentToken, "%",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.BacktickToken, "`",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.BackslashToken, "\\",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.OpenBracketToken, "[",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.CloseBracketToken, "]",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.SingleQuoteToken, "'",));
        //TryAddSymbol(punctuationTokens, new SyntaxSymbol(SyntaxKind.DoubleQuoteToken, "\"",));

        return punctuationTokens;
    }

    internal static List<SyntaxSymbol> LoadScopeDelimiters()
    {
        var scopeKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(scopeKeywords, new SyntaxSymbol(SyntaxKind.OpenParenthesisToken, "("));
        TryAddSymbol(scopeKeywords,new SyntaxSymbol(SyntaxKind.CloseParenthesisToken, ")"));
        TryAddSymbol(scopeKeywords,new SyntaxSymbol(SyntaxKind.OpenBraceToken, "{"));
        TryAddSymbol(scopeKeywords,new SyntaxSymbol(SyntaxKind.CloseBraceToken, "}"));
        return scopeKeywords;
    }

    // Load Syntax Token Operators
    internal static List<SyntaxSymbol> LoadAssignmentOperators()
    {
        var assignmentOperators = new List<SyntaxSymbol>();
        assignmentOperators.Add(new SyntaxSymbol(SyntaxKind.EqualsToken, "="));
        return assignmentOperators;
    }

    internal static List<SyntaxSymbol> LoadPrecedenceOperators()
    {
        var operators = new List<SyntaxSymbol>();
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.StarToken, "*", 5));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.SlashToken, "/", 5));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.PlusToken, "+", 4, 6));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.MinusToken, "-", 4, 6));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.EqualsEqualsToken, "==", 3));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.BangEqualsToken, "!=", 3));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.GreaterOrEqualsToken, ">=", 3));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.GreaterToken, ">", 3));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.LessOrEqualsToken, "<=", 3));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.LessToken, "<", 3));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.AmpersandAmpersandToken, "&&", 2));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.AmpersandToken, "&", 2));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.PipeToken, "|", 1));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.PipePipeToken, "||", 1));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.HatToken, "^", 1));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.TildeToken, "~", 0, 6));
        TryAddSymbol(operators, new SyntaxSymbol(SyntaxKind.BangToken, "!", 0, 6));
        return operators;
    }

    // Load Syntax Token Keywords
    internal static List<SyntaxSymbol> LoadStatementKeywords()
    {
        var statementKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.IfKeyword, "if"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.ElseKeyword, "else"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.ElseIfKeyword, "elseif"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.WhileKeyword, "while"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.ForKeyword, "for"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.ToKeyword, "to"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.SwitchKeyword, "switch"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.SwitchCaseKeyword, "case"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.SwitchDefaultKeyword, "default"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.MatchKeyword, "match"));

        return statementKeywords;
    }
    internal static List<SyntaxSymbol> LoadBooleanKeywords()
    {
        var booleanKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(booleanKeywords, new SyntaxSymbol(SyntaxKind.TrueKeyword, "true"));
        TryAddSymbol(booleanKeywords, new SyntaxSymbol(SyntaxKind.FalseKeyword, "false"));
        return booleanKeywords;
    }       
    internal static List<SyntaxSymbol> LoadFlowControlKeywords()
    {
        var flowControlKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.ContinueKeyword, "continue"));
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.BreakKeyword, "break"));
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.EndKeyword, "end"));

        return flowControlKeywords;
    }
    internal static List<SyntaxSymbol> LoadVariableKeywords()
    {
        var variableKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(variableKeywords,new SyntaxSymbol(SyntaxKind.LetKeyword, "let"));
        TryAddSymbol(variableKeywords, new SyntaxSymbol(SyntaxKind.VarKeyword, "var"));
        return variableKeywords;
    }

}
