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
        TryAddSymbol(assignmentOperators, new SyntaxSymbol(SyntaxKind.EqualsToken, "="));
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
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.DoKeyword, "do"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.ForKeyword, "for"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.ToKeyword, "to"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.SwitchKeyword, "switch"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.SwitchCaseKeyword, "case"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.SwitchDefaultKeyword, "default"));
        TryAddSymbol(statementKeywords, new SyntaxSymbol(SyntaxKind.MatchKeyword, "match"));

        return statementKeywords;
    }
     
    internal static List<SyntaxSymbol> LoadFlowControlKeywords()
    {
        var flowControlKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.ContinueKeyword, "continue"));
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.BreakKeyword, "break"));
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.EndKeyword, "end"));
        TryAddSymbol(flowControlKeywords, new SyntaxSymbol(SyntaxKind.ReturnKeyword, "return"));

        return flowControlKeywords;
    }
    internal static List<SyntaxSymbol> LoadBooleanKeywords()
    {
        var booleanKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(booleanKeywords, new SyntaxSymbol(SyntaxKind.TrueKeyword, "true"));
        TryAddSymbol(booleanKeywords, new SyntaxSymbol(SyntaxKind.FalseKeyword, "false"));
        return booleanKeywords;
    }
    internal static List<SyntaxSymbol> LoadVariableKeywords()
    {
        var variableKeywords = new List<SyntaxSymbol>();
        TryAddSymbol(variableKeywords,new SyntaxSymbol(SyntaxKind.LetKeyword, "let"));
        TryAddSymbol(variableKeywords, new SyntaxSymbol(SyntaxKind.VarKeyword, "var"));
        return variableKeywords;
    }

    internal static List<SyntaxSymbol> LoadPrimitiveKeywords()
    {
        var primitiveKeywords = new List<SyntaxSymbol>();

        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.CharKeyword, "char"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.BoolKeyword, "bool"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.ByteKeyword, "byte"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.SByteKeyword, "sbyte"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.ShortKeyword, "short"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.UShortKeyword, "ushort"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.IntKeyword, "int"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.UIntKeyword, "uint"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.LongKeyword, "long"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.ULongKeyword, "ulong"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.FloatKeyword, "float"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.DoubleKeyword, "double"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.DecimalKeyword, "decimal"));
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.StringKeyword, "string"));        
        TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.VoidKeyword, "void"));
        //TryAddSymbol(primitiveKeywords, new SyntaxSymbol(SyntaxKind.ObjectKeyword, "object"));

        return primitiveKeywords;
    }

    internal static List<SyntaxSymbol> LoadConstructKeywords()
    {
        var constructKeywords = new List<SyntaxSymbol>();

        //// Type definition keywords
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ClassKeyword, "class"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.InterfaceKeyword, "interface"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.StructKeyword, "struct"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.EnumKeyword, "enum"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.DelegateKeyword, "delegate"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.RecordKeyword, "record"));

        //// Namespace keyword
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.NamespaceKeyword, "namespace"));

        //// Access modifiers
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.PublicKeyword, "public"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.PrivateKeyword, "private"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.InternalKeyword, "internal"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ProtectedKeyword, "protected"));

        //// Member keywords
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.StaticKeyword, "static"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ReadOnlyKeyword, "readonly"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ConstKeyword, "const"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.VirtualKeyword, "virtual"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.AbstractKeyword, "abstract"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.SealedKeyword, "sealed"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.OverrideKeyword, "override"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.NewKeyword, "new"));

        //// Property/event keywords
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.GetKeyword, "get"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.SetKeyword, "set"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.InitKeyword, "init"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.EventKeyword, "event"));

        //// Exception handling
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.TryKeyword, "try"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.CatchKeyword, "catch"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.FinallyKeyword, "finally"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ThrowKeyword, "throw"));

        //// Other keywords
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.UsingKeyword, "using"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ReturnKeyword, "return"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.BaseKeyword, "base"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.ThisKeyword, "this"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.NullKeyword, "null"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.AsKeyword, "as"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.IsKeyword, "is"));
        //TryAddSymbol(constructKeywords, new SyntaxSymbol(SyntaxKind.InKeyword, "in"));

        return constructKeywords;
    }
}
