using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.ObjectModel;

namespace Minsk.CodeAnalysis.Syntax;

public static class SyntaxFacts
{
    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    {
        if (SyntaxIndex.UnaryOperators.TryGetValue(kind, out var syntaxType))
            return syntaxType.Precedence;

        return 0;
    }

    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        if (SyntaxIndex.BinaryOperators.TryGetValue(kind, out var syntaxType))
            return syntaxType.Precedence;

        return 0;
    }

    public static SyntaxKind GetKeywordKind(string text)
    {
        // Check all keyword dictionaries
        var allKeywords = SyntaxIndex.ReservedKeywords;

        foreach (var kvp in allKeywords)
        {

            if (kvp.Value.Text == text)
                return kvp.Key;
        }

        return SyntaxKind.IdentifierToken;
    }

    public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
    {
        return SyntaxIndex.UnaryOperators.Keys;
    }

    public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
    {
        return SyntaxIndex.BinaryOperators.Keys;
    }

    public static string? GetText(SyntaxKind kind)
    {
        // Try to get from the combined index first
        if (SyntaxIndex.BuildSyntaxKindIndex().TryGetValue(kind, out var syntaxType))
            return syntaxType.Text;

        return null;
    }
}