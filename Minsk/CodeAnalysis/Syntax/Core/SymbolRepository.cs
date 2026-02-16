using Minsk.CodeAnalysis.Symbols;

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
        return SyntaxDefinitions.LoadAssignmentOperators()
            .Concat(SyntaxDefinitions.LoadPrecedenceOperators())
            .ToList();
    }

    public static List<SyntaxSymbol> LoadKeywords()
    {
        return SyntaxDefinitions.LoadStatementKeywords()
            .Concat(SyntaxDefinitions.LoadBooleanKeywords())
            .Concat(SyntaxDefinitions.LoadFlowControlKeywords())
            .Concat(SyntaxDefinitions.LoadVariableKeywords())
            .ToList();
    }

    public static List<SyntaxSymbol> LoadDelimiters()
    {
        return SyntaxDefinitions.LoadPunctuationDelimiters()
            .Concat(SyntaxDefinitions.LoadScopeDelimiters())
            .ToList();
    }
}
