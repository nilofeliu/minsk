using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Core;

internal class SymbolRepository
{
    private static readonly Lazy<SymbolRepository> _instance = new(() => new SymbolRepository());
    public static SymbolRepository Instance => _instance.Value;

    private static SyntaxRegistry _registry => SyntaxRegistry.Instance;

    private SymbolRepository()
    {
        _registry.Register(SymbolTable.Operators, LoadOperators());
        _registry.Register(SymbolTable.Keywords, LoadKeywords());
        _registry.Register(SymbolTable.Delimiters, LoadDelimiters());
    }

    public static List<SyntaxSymbol> LoadOperators()
    {
        return SymbolDefinitions.LoadAssignmentOperators()
            .Concat(SymbolDefinitions.LoadPrecedenceOperators())
            .ToList();
    }

    public static List<SyntaxSymbol> LoadKeywords()
    {
        return SymbolDefinitions.LoadStatementKeywords()
            .Concat(SymbolDefinitions.LoadBooleanKeywords())
            .Concat(SymbolDefinitions.LoadFlowControlKeywords())
            .Concat(SymbolDefinitions.LoadVariableKeywords())
            .ToList();
    }

    public static List<SyntaxSymbol> LoadDelimiters()
    {
        return SymbolDefinitions.LoadPunctuationDelimiters()
            .Concat(SymbolDefinitions.LoadScopeDelimiters())
            .ToList();
    }

}
    internal static class SymbolDefinitions
    { 
    // Load Syntax Token Delimiters
    internal static List<SyntaxSymbol> LoadPunctuationDelimiters()
    {
        var punctuationTokens = new List<SyntaxSymbol>();
        punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.ColonToken, ":"));

        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.CommaToken, ",",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.SemicolonToken, ";",));
        // punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.UnderscoreToken, "_",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.DotToken, ".",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.QuestionToken, "?",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.ExclamationToken, "!",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.AtToken, "@",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.HashToken, "#",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.DollarToken, "$",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.PercentToken, "%",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.BacktickToken, "`",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.BackslashToken, "\\",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.OpenBracketToken, "[",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.CloseBracketToken, "]",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.SingleQuoteToken, "'",));
        //punctuationTokens.Add(new SyntaxSymbol(SyntaxKind.DoubleQuoteToken, "\"",));

        return punctuationTokens;
    }
    internal static List<SyntaxSymbol> LoadScopeDelimiters()
    {
        var scopeKeywords = new List<SyntaxSymbol>();
        scopeKeywords.Add(new SyntaxSymbol(SyntaxKind.OpenParenthesisToken, "("));
        scopeKeywords.Add(new SyntaxSymbol(SyntaxKind.CloseParenthesisToken, ")"));
        scopeKeywords.Add(new SyntaxSymbol(SyntaxKind.OpenBraceToken, "{"));
        scopeKeywords.Add(new SyntaxSymbol(SyntaxKind.CloseBraceToken, "}"));
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
        operators.Add(new SyntaxSymbol(SyntaxKind.StarToken, "*", 5));
        operators.Add(new SyntaxSymbol(SyntaxKind.SlashToken, "/", 5));
        operators.Add(new SyntaxSymbol(SyntaxKind.PlusToken, "+", 4, 6));
        operators.Add(new SyntaxSymbol(SyntaxKind.MinusToken, "-", 4, 6));
        operators.Add(new SyntaxSymbol(SyntaxKind.EqualsEqualsToken, "==", 3));
        operators.Add(new SyntaxSymbol(SyntaxKind.BangEqualsToken, "!=", 3));
        operators.Add(new SyntaxSymbol(SyntaxKind.GreaterOrEqualsToken, ">=", 3));
        operators.Add(new SyntaxSymbol(SyntaxKind.GreaterToken, ">", 3));
        operators.Add(new SyntaxSymbol(SyntaxKind.LessOrEqualsToken, "<=", 3));
        operators.Add(new SyntaxSymbol(SyntaxKind.LessToken, "<", 3));
        operators.Add(new SyntaxSymbol(SyntaxKind.AmpersandAmpersandToken, "&&", 2));
        operators.Add(new SyntaxSymbol(SyntaxKind.AmpersandToken, "&", 2));
        operators.Add(new SyntaxSymbol(SyntaxKind.PipeToken, "|", 1));
        operators.Add(new SyntaxSymbol(SyntaxKind.PipePipeToken, "||", 1));
        operators.Add(new SyntaxSymbol(SyntaxKind.HatToken, "^", 1));
        operators.Add(new SyntaxSymbol(SyntaxKind.TildeToken, "~", 0, 6));
        operators.Add(new SyntaxSymbol(SyntaxKind.BangToken, "!", 0, 6));
        return operators;
    }

    // Load Syntax Token Keywords
    internal static List<SyntaxSymbol> LoadStatementKeywords()
    {
        var statementKeywords = new List<SyntaxSymbol>();
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.IfKeyword, "if"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.ElseKeyword, "else"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.ElseIfKeyword, "elseif"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.WhileKeyword, "while"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.ForKeyword, "for"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.ToKeyword, "to"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.SwitchKeyword, "switch"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.SwitchCaseKeyword, "case"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.SwitchDefaultKeyword, "default"));
        statementKeywords.Add(new SyntaxSymbol(SyntaxKind.MatchKeyword, "match"));

        return statementKeywords;
    }
    internal static List<SyntaxSymbol> LoadBooleanKeywords()
    {
        var booleanKeywords = new List<SyntaxSymbol>();
        booleanKeywords.Add(new SyntaxSymbol(SyntaxKind.TrueKeyword, "true"));
        booleanKeywords.Add(new SyntaxSymbol(SyntaxKind.FalseKeyword, "false"));
        return booleanKeywords;
    }       
    internal static List<SyntaxSymbol> LoadFlowControlKeywords()
    {
        var flowControlKeywords = new List<SyntaxSymbol>();
        flowControlKeywords.Add(new SyntaxSymbol(SyntaxKind.ContinueKeyword, "continue"));
        flowControlKeywords.Add(new SyntaxSymbol(SyntaxKind.BreakKeyword, "break"));
        flowControlKeywords.Add(new SyntaxSymbol(SyntaxKind.EndKeyword, "end"));

        return flowControlKeywords;
    }
    internal static List<SyntaxSymbol> LoadVariableKeywords()
    {
        var variableKeywords = new List<SyntaxSymbol>();
        variableKeywords.Add(new SyntaxSymbol(SyntaxKind.LetKeyword, "let"));
        variableKeywords.Add(new SyntaxSymbol(SyntaxKind.VarKeyword, "var"));
        return variableKeywords;
    }

}
