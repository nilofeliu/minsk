using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.ObjectModel;

namespace Minsk.CodeAnalysis.Syntax;

public static class SyntaxFacts
{
    static SyntaxIndex syntaxIndex = SyntaxIndex.Instance;

    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    {
        if (syntaxIndex.UnaryOperators.TryGetValue(kind, out var syntaxType))
            return syntaxType.Precedence;

        return 0;
    }

    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        if (syntaxIndex.BinaryOperators.TryGetValue(kind, out var syntaxType))
            return syntaxType.Precedence;

        return 0;
    }

    public static SyntaxKind GetKeywordKind(string text)
    {

        var result = syntaxIndex.ReservedKeywords
            .FirstOrDefault(kvp => kvp.Value.Text == text);

        return result.Value != null ? result.Key : SyntaxKind.IdentifierToken;

    }

    public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
    {
        return syntaxIndex.UnaryOperators.Keys;
    }

    public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
    {
        return syntaxIndex.BinaryOperators.Keys;
    }

    public static string? GetText(SyntaxKind kind)
    {
        // Try to get from the combined index first
        if (syntaxIndex.SyntaxKindIndex.TryGetValue(kind, out var syntaxType))
            return syntaxType;

        return null;
    }
}