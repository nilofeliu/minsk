using Minsk.CodeAnalysis.Syntax.Kind;
using System.Collections.ObjectModel;

namespace Minsk.CodeAnalysis.Syntax.Core;

public static class SyntaxQuery
{
    // private static SyntaxIndex _syntaxIndex = SyntaxIndex.Instance;

    private static SyntaxRegistry _registry = SyntaxRegistry.Instance;
    
    private static SymbolRepository _repository = SymbolRepository.Instance;
    

    public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
    {
        var output = _registry.GetSymbolsByCategory(SymbolTable.Operators)
            .Where(s => s.UnaryPrecedence > 0)
            .Select(s => s.Kind);

        return output;
    }

    public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
    {
        var output = _registry.GetSymbolsByCategory(SymbolTable.Operators)
            .Where(s => s.BinaryPrecedence > 0)
            .Select(s => s.Kind);

        return output;
    }

    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
    {
        var symbol = _registry.GetSymbolsByCategory(SymbolTable.Operators)
            .Where(s => s.UnaryPrecedence > 0)
            .FirstOrDefault(s => s.Kind == kind);

        return symbol?.UnaryPrecedence ?? 0;
    }

    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        var symbol = _registry.GetSymbolsByCategory(SymbolTable.Operators)
            .Where(s => s.BinaryPrecedence > 0)
            .FirstOrDefault(s => s.Kind == kind);

        return symbol?.BinaryPrecedence ?? 0;
    }

    public static SyntaxKind GetKeywordKind(string text)
    {
        return _registry.KindIndex.TryGetValue(text, out var kind)
            ? kind
            : SyntaxKind.IdentifierToken;
    }

    public static Dictionary<string, SyntaxKind> GetTokenIndex()
    {
        return _registry.KindIndex;
    }

    public static string? GetText(SyntaxKind kind)
    {
        // Try to get from the combined index first
        if (_registry.TextIndex.TryGetValue(kind, out var syntaxType))
            return syntaxType;

        return null;
    }
       
}