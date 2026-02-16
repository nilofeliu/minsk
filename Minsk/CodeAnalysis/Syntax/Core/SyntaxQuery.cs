using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.ObjectModel;

namespace Minsk.CodeAnalysis.Syntax.Core;



public static class SyntaxQuery
{
    private static SyntaxIndex _syntaxIndex = SyntaxIndex.Instance;

    //public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    //{
    //    if (_syntaxIndex.UnaryOperators.TryGetValue(kind, out var syntaxType))
    //        return syntaxType.UnaryPrecedence;

    //    return 0;
    //}

    //public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    //{
    //    if (_syntaxIndex.BinaryOperators.TryGetValue(kind, out var syntaxType))
    //        return syntaxType.BinaryPrecedence;

    //    return 0;
    //}

    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    {
        if (_syntaxIndex.Operators.TryGetValue(kind, out var syntaxSymbol))
            return syntaxSymbol.UnaryPrecedence;
        return 0;
    }

    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        if (_syntaxIndex.Operators.TryGetValue(kind, out var syntaxSymbol))
            return syntaxSymbol.BinaryPrecedence;
        return 0;
    }

    public static SyntaxKind GetKeywordKind(string text)
    {
        return _syntaxIndex.TextToKind.TryGetValue(text, out var kind)
            ? kind
            : SyntaxKind.IdentifierToken;
    }


    //public static SyntaxKind GetKeywordKind(string text)
    //{

    //    var result = _syntaxIndex.ReservedKeywords
    //        .FirstOrDefault(kvp => kvp.Value.Text == text);

    //    return result.Value != null ? result.Key : SyntaxKind.IdentifierToken;

    //}

    public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
    {
        return _syntaxIndex.Operators
            .Where(op => op.Value.UnaryPrecedence > 0)
            .Select(op => op.Key);
    }

    public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
    {
        return _syntaxIndex.Operators
            .Where(op => op.Value.BinaryPrecedence > 0)
            .Select(op => op.Key); ;
    }

    public static Dictionary<string, SyntaxKind> GetTokenIndex()
    {
        return _syntaxIndex.TokenIndex;
    }

    public static string? GetText(SyntaxKind kind)
    {
        // Try to get from the combined index first
        if (_syntaxIndex.SyntaxKindIndex.TryGetValue(kind, out var syntaxType))
            return syntaxType;

        return null;
    }
}