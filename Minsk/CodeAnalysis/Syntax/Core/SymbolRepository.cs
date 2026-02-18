using Minsk.CodeAnalysis.Symbols;
using System.Linq;

namespace Minsk.CodeAnalysis.Syntax.Core;

internal class SymbolRepository
{
    private static readonly Lazy<SymbolRepository> _instance = new(() => new SymbolRepository());
    public static SymbolRepository Instance => _instance.Value;

    private static SyntaxRegistry _registry => SyntaxRegistry.Instance;

    private SymbolRepository()
    {
        _registry.Register(SymbolTable.Operators, LoadOperators());
        _registry.Register(SymbolTable.SystemKeywords, LoadKeywords());
        _registry.Register(SymbolTable.ControlKeywords, LoadControlKeywords());
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
        return SyntaxDefinitions.LoadBooleanKeywords()
            .Concat(SyntaxDefinitions.LoadVariableKeywords())
            .Concat(SyntaxDefinitions.LoadPrimitiveKeywords())
            .ToList();
    }

    public static List<SyntaxSymbol> LoadControlKeywords()
    {
        return SyntaxDefinitions.LoadStatementKeywords()
            .Concat(SyntaxDefinitions.LoadFlowControlKeywords())
            .ToList();
    }

    public static List<SyntaxSymbol> LoadDelimiters()
    {
        return SyntaxDefinitions.LoadPunctuationDelimiters()
            .Concat(SyntaxDefinitions.LoadScopeDelimiters())
            .ToList();
    }
}
