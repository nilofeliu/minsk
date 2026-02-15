using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;

namespace Minsk.CodeAnalysis.Syntax.Core;

public static class SyntaxDefinitions
{
    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadAssignmentOperators()
    {
        var assignmentOperators = new Dictionary<SyntaxKind, SyntaxSymbol>();
        assignmentOperators.Add(SyntaxKind.EqualsToken, new SyntaxSymbol(SyntaxKind.EqualsToken, "=", 0));
        return assignmentOperators;
    }

    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadBooleanKeywords()
    {
        var booleanKeywords = new Dictionary<SyntaxKind, SyntaxSymbol>();
        booleanKeywords.Add(SyntaxKind.TrueKeyword, new SyntaxSymbol(SyntaxKind.TrueKeyword, "true", 0));
        booleanKeywords.Add(SyntaxKind.FalseKeyword, new SyntaxSymbol(SyntaxKind.FalseKeyword, "false", 0));
        return booleanKeywords;
    }

    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadScopeKeywords()
    {
        var scopeKeywords = new Dictionary<SyntaxKind, SyntaxSymbol>();
        scopeKeywords.Add(SyntaxKind.OpenParenthesisToken, new SyntaxSymbol(SyntaxKind.OpenParenthesisToken, "(", 0));
        scopeKeywords.Add(SyntaxKind.CloseParenthesisToken, new SyntaxSymbol(SyntaxKind.CloseParenthesisToken, ")", 0));
        scopeKeywords.Add(SyntaxKind.OpenBraceToken, new SyntaxSymbol(SyntaxKind.OpenBraceToken, "{", 0));
        scopeKeywords.Add(SyntaxKind.CloseBraceToken, new SyntaxSymbol(SyntaxKind.CloseBraceToken, "}", 0));
        return scopeKeywords;
    }


    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadStatementKeywords()
    {
        var statementKeywords = new Dictionary<SyntaxKind, SyntaxSymbol>();
        statementKeywords.Add(SyntaxKind.IfKeyword, new SyntaxSymbol(SyntaxKind.IfKeyword, "if", 0));
        statementKeywords.Add(SyntaxKind.ElseKeyword, new SyntaxSymbol(SyntaxKind.ElseKeyword, "else", 0));
        statementKeywords.Add(SyntaxKind.ElseIfKeyword, new SyntaxSymbol(SyntaxKind.ElseIfKeyword, "elseif", 0));
        statementKeywords.Add(SyntaxKind.WhileKeyword, new SyntaxSymbol(SyntaxKind.WhileKeyword, "while", 0));
        statementKeywords.Add(SyntaxKind.ForKeyword, new SyntaxSymbol(SyntaxKind.ForKeyword, "for", 0));
        statementKeywords.Add(SyntaxKind.ToKeyword, new SyntaxSymbol(SyntaxKind.ToKeyword, "to", 0));
        statementKeywords.Add(SyntaxKind.SwitchKeyword, new SyntaxSymbol(SyntaxKind.SwitchKeyword, "switch", 0));
        statementKeywords.Add(SyntaxKind.SwitchCaseKeyword, new SyntaxSymbol(SyntaxKind.SwitchCaseKeyword, "case", 0));
        statementKeywords.Add(SyntaxKind.SwitchDefaultKeyword, new SyntaxSymbol(SyntaxKind.SwitchDefaultKeyword, "default", 0));
        statementKeywords.Add(SyntaxKind.MatchKeyword, new SyntaxSymbol(SyntaxKind.MatchKeyword, "match", 0));


        return statementKeywords;
    }

    // All loading methods remain unchanged — just private now
    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadUnaryOperators()
    {
        var unaryOperators = new Dictionary<SyntaxKind, SyntaxSymbol>();
        unaryOperators.Add(SyntaxKind.PlusToken, new SyntaxSymbol(SyntaxKind.PlusToken, "+", 6));
        unaryOperators.Add(SyntaxKind.MinusToken, new SyntaxSymbol(SyntaxKind.MinusToken, "-", 6));
        unaryOperators.Add(SyntaxKind.BangToken, new SyntaxSymbol(SyntaxKind.BangToken, "!", 6));
        unaryOperators.Add(SyntaxKind.TildeToken, new SyntaxSymbol(SyntaxKind.TildeToken, "~", 6));
        return unaryOperators;
    }

    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadBinaryOperators()
    {
        var binaryOperators = new Dictionary<SyntaxKind, SyntaxSymbol>();
        binaryOperators.Add(SyntaxKind.StarToken, new SyntaxSymbol(SyntaxKind.StarToken, "*", 5));
        binaryOperators.Add(SyntaxKind.SlashToken, new SyntaxSymbol(SyntaxKind.SlashToken, "/", 5));
        binaryOperators.Add(SyntaxKind.PlusToken, new SyntaxSymbol(SyntaxKind.PlusToken, "+", 4));
        binaryOperators.Add(SyntaxKind.MinusToken, new SyntaxSymbol(SyntaxKind.MinusToken, "-", 4));
        binaryOperators.Add(SyntaxKind.EqualsEqualsToken, new SyntaxSymbol(SyntaxKind.EqualsEqualsToken, "==", 3));
        binaryOperators.Add(SyntaxKind.BangEqualsToken, new SyntaxSymbol(SyntaxKind.BangEqualsToken, "!=", 3));
        binaryOperators.Add(SyntaxKind.GreaterOrEqualsToken, new SyntaxSymbol(SyntaxKind.GreaterOrEqualsToken, ">=", 3));
        binaryOperators.Add(SyntaxKind.GreaterToken, new SyntaxSymbol(SyntaxKind.GreaterToken, ">", 3));
        binaryOperators.Add(SyntaxKind.LessOrEqualsToken, new SyntaxSymbol(SyntaxKind.LessOrEqualsToken, "<=", 3));
        binaryOperators.Add(SyntaxKind.LessToken, new SyntaxSymbol(SyntaxKind.LessToken, "<", 3));
        binaryOperators.Add(SyntaxKind.AmpersandAmpersandToken, new SyntaxSymbol(SyntaxKind.AmpersandAmpersandToken, "&&", 2));
        binaryOperators.Add(SyntaxKind.AmpersandToken, new SyntaxSymbol(SyntaxKind.AmpersandToken, "&", 2));
        binaryOperators.Add(SyntaxKind.PipeToken, new SyntaxSymbol(SyntaxKind.PipeToken, "|", 1));
        binaryOperators.Add(SyntaxKind.PipePipeToken, new SyntaxSymbol(SyntaxKind.PipePipeToken, "||", 1));
        binaryOperators.Add(SyntaxKind.HatToken, new SyntaxSymbol(SyntaxKind.HatToken, "^", 1));
        return binaryOperators;
    }

    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadFlowControlKeywords()
    {
        var flowControlKeywords = new Dictionary<SyntaxKind, SyntaxSymbol>();
        flowControlKeywords.Add(SyntaxKind.ContinueKeyword, new SyntaxSymbol(SyntaxKind.ContinueKeyword, "continue", 0));
        flowControlKeywords.Add(SyntaxKind.BreakKeyword, new SyntaxSymbol(SyntaxKind.BreakKeyword, "break", 0));
        flowControlKeywords.Add(SyntaxKind.EndKeyword, new SyntaxSymbol(SyntaxKind.EndKeyword, "end", 0));

        return flowControlKeywords;
    }
    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadVariableKeywords()
    {
        var variableKeywords = new Dictionary<SyntaxKind, SyntaxSymbol>();
        variableKeywords.Add(SyntaxKind.LetKeyword, new SyntaxSymbol(SyntaxKind.LetKeyword, "let", 0));
        variableKeywords.Add(SyntaxKind.VarKeyword, new SyntaxSymbol(SyntaxKind.VarKeyword, "var", 0));
        return variableKeywords;
    }

    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadTypeKeywords()
    {
        var typeKeywords = new Dictionary<SyntaxKind, SyntaxSymbol>();

        //    // Built-in value types
        typeKeywords.Add(SyntaxKind.CharKeyword, new SyntaxSymbol(SyntaxKind.CharKeyword, "char", 0));
        typeKeywords.Add(SyntaxKind.BoolKeyword, new SyntaxSymbol(SyntaxKind.BoolKeyword, "bool", 0));
        typeKeywords.Add(SyntaxKind.ByteKeyword, new SyntaxSymbol(SyntaxKind.ByteKeyword, "byte", 0));
        typeKeywords.Add(SyntaxKind.SByteKeyword, new SyntaxSymbol(SyntaxKind.SByteKeyword, "sbyte", 0));
        typeKeywords.Add(SyntaxKind.ShortKeyword, new SyntaxSymbol(SyntaxKind.ShortKeyword, "short", 0));
        typeKeywords.Add(SyntaxKind.UShortKeyword, new SyntaxSymbol(SyntaxKind.UShortKeyword, "ushort", 0));
        typeKeywords.Add(SyntaxKind.IntKeyword, new SyntaxSymbol(SyntaxKind.IntKeyword, "int", 0));
        typeKeywords.Add(SyntaxKind.UIntKeyword, new SyntaxSymbol(SyntaxKind.UIntKeyword, "uint", 0));
        typeKeywords.Add(SyntaxKind.LongKeyword, new SyntaxSymbol(SyntaxKind.LongKeyword, "long", 0));
        typeKeywords.Add(SyntaxKind.ULongKeyword, new SyntaxSymbol(SyntaxKind.ULongKeyword, "ulong", 0));
        typeKeywords.Add(SyntaxKind.FloatKeyword, new SyntaxSymbol(SyntaxKind.FloatKeyword, "float", 0));
        typeKeywords.Add(SyntaxKind.DoubleKeyword, new SyntaxSymbol(SyntaxKind.DoubleKeyword, "double", 0));
        typeKeywords.Add(SyntaxKind.DecimalKeyword, new SyntaxSymbol(SyntaxKind.DecimalKeyword, "decimal", 0));

        //    // Built-in reference types
        typeKeywords.Add(SyntaxKind.StringKeyword, new SyntaxSymbol(SyntaxKind.StringKeyword, "string", 0));
        typeKeywords.Add(SyntaxKind.ObjectKeyword, new SyntaxSymbol(SyntaxKind.ObjectKeyword, "object", 0));

        //    // Special types
        typeKeywords.Add(SyntaxKind.VoidKeyword, new SyntaxSymbol(SyntaxKind.VoidKeyword, "void", 0));

        // .NET Framework type aliases (System.*)

        typeKeywords.Add(SyntaxKind.SingleKeyword, new SyntaxSymbol(SyntaxKind.SingleKeyword, "single", 0)); // System.Single
        typeKeywords.Add(SyntaxKind.Int8Keyword, new SyntaxSymbol(SyntaxKind.Int8Keyword, "int8", 0));    // System.SByte
        typeKeywords.Add(SyntaxKind.Int16Keyword, new SyntaxSymbol(SyntaxKind.Int16Keyword, "int16", 0));  // System.Int16
        typeKeywords.Add(SyntaxKind.Int32Keyword, new SyntaxSymbol(SyntaxKind.Int32Keyword, "int32", 0));  // System.Int32
        typeKeywords.Add(SyntaxKind.Int64Keyword, new SyntaxSymbol(SyntaxKind.Int64Keyword, "int64", 0));  // System.Int64
        typeKeywords.Add(SyntaxKind.UInt8Keyword, new SyntaxSymbol(SyntaxKind.UInt8Keyword, "uint8", 0));  // System.Byte
        typeKeywords.Add(SyntaxKind.UInt16Keyword, new SyntaxSymbol(SyntaxKind.UInt16Keyword, "uint16", 0)); // System.UInt16
        typeKeywords.Add(SyntaxKind.UInt32Keyword, new SyntaxSymbol(SyntaxKind.UInt32Keyword, "uint32", 0)); // System.UInt32
        typeKeywords.Add(SyntaxKind.UInt64Keyword, new SyntaxSymbol(SyntaxKind.UInt64Keyword, "uint64", 0)); // System.UInt64


        return typeKeywords;
    }

    internal static Dictionary<SyntaxKind, SyntaxSymbol> LoadPunctuationOperators()
    {
        var punctuationTokens = new Dictionary<SyntaxKind, SyntaxSymbol>();
        //punctuationTokens.Add(SyntaxKind.CommaToken, new SyntaxSymbol(SyntaxKind.CommaToken, ",", 0));
        //punctuationTokens.Add(SyntaxKind.SemicolonToken, new SyntaxSymbol(SyntaxKind.SemicolonToken, ";", 0));
        punctuationTokens.Add(SyntaxKind.ColonToken, new SyntaxSymbol(SyntaxKind.ColonToken, ":", 0));
       // punctuationTokens.Add(SyntaxKind.UnderscoreToken, new SyntaxSymbol(SyntaxKind.UnderscoreToken, "_", 0));
        //punctuationTokens.Add(SyntaxKind.DotToken, new SyntaxSymbol(SyntaxKind.DotToken, ".", 0));
        //punctuationTokens.Add(SyntaxKind.QuestionToken, new SyntaxSymbol(SyntaxKind.QuestionToken, "?", 0));
        //punctuationTokens.Add(SyntaxKind.ExclamationToken, new SyntaxSymbol(SyntaxKind.ExclamationToken, "!", 0));
        //punctuationTokens.Add(SyntaxKind.AtToken, new SyntaxSymbol(SyntaxKind.AtToken, "@", 0));
        //punctuationTokens.Add(SyntaxKind.HashToken, new SyntaxSymbol(SyntaxKind.HashToken, "#", 0));
        //punctuationTokens.Add(SyntaxKind.DollarToken, new SyntaxSymbol(SyntaxKind.DollarToken, "$", 0));
        //punctuationTokens.Add(SyntaxKind.PercentToken, new SyntaxSymbol(SyntaxKind.PercentToken, "%", 0));
        //punctuationTokens.Add(SyntaxKind.BacktickToken, new SyntaxSymbol(SyntaxKind.BacktickToken, "`", 0));
        //punctuationTokens.Add(SyntaxKind.BackslashToken, new SyntaxSymbol(SyntaxKind.BackslashToken, "\\", 0));
        //punctuationTokens.Add(SyntaxKind.OpenBracketToken, new SyntaxSymbol(SyntaxKind.OpenBracketToken, "[", 0));
        //punctuationTokens.Add(SyntaxKind.CloseBracketToken, new SyntaxSymbol(SyntaxKind.CloseBracketToken, "]", 0));
        //punctuationTokens.Add(SyntaxKind.SingleQuoteToken, new SyntaxSymbol(SyntaxKind.SingleQuoteToken, "'", 0));
        //punctuationTokens.Add(SyntaxKind.DoubleQuoteToken, new SyntaxSymbol(SyntaxKind.DoubleQuoteToken, "\"", 0));
        return punctuationTokens;
    }
}