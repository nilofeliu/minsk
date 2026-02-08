using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Object;

namespace Minsk.CodeAnalysis.Syntax;

public class SyntaxIndex
{
    // Static dictionaries for each category
    private static readonly Dictionary<SyntaxKind, SyntaxType> _unaryOperators;
    private static readonly Dictionary<SyntaxKind, SyntaxType> _binaryOperators;
    private static readonly Dictionary<SyntaxKind, SyntaxType> _reservedKeywords;

    // Combined dictionary for all syntax kinds
    private static readonly Dictionary<SyntaxKind, SyntaxType> _syntaxKindIndex;

    // Static constructor for one-time initialization
    static SyntaxIndex()
    {
        _unaryOperators = LoadUnaryOperators();
        _binaryOperators = LoadBinaryOperators();
        _reservedKeywords = LoadReservedKeywords();

        // Build the combined index
        _syntaxKindIndex = BuildSyntaxKindIndex();
    }

    // Public properties for each category
    public static Dictionary<SyntaxKind, SyntaxType> UnaryOperators => _unaryOperators;
    public static Dictionary<SyntaxKind, SyntaxType> BinaryOperators => _binaryOperators;
    public static Dictionary<SyntaxKind, SyntaxType> ReservedKeywords => _reservedKeywords;

    private static Dictionary<SyntaxKind, SyntaxType> LoadReservedKeywords()
    {
        return LoadBooleanKeywords()
            .Concat(LoadStatementKeywords())
            .Concat(LoadVariableKeywords())
            .Concat(LoadScopeKeywords())
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
    
    internal static Dictionary<SyntaxKind, SyntaxType> BuildSyntaxKindIndex()
    {
        var combined = new Dictionary<SyntaxKind, SyntaxType>();

        // Merge all dictionaries, handling duplicates (like PlusToken, MinusToken)
        MergeDictionary(combined, _unaryOperators);
        MergeDictionary(combined, _binaryOperators);
        MergeDictionary(combined, _reservedKeywords);

        return combined;
    }

    private static void MergeDictionary(
        Dictionary<SyntaxKind, SyntaxType> target,
        Dictionary<SyntaxKind, SyntaxType> source)
    {
        foreach (var kvp in source)
        {
            // For tokens that appear in multiple categories (like PlusToken),
            // you need to decide which one to keep or handle differently
            if (!target.ContainsKey(kvp.Key))
            {
                target.Add(kvp.Key, kvp.Value);
            }
            // Optional: Handle duplicates based on your needs
            // else if (target[kvp.Key].Precedence < kvp.Value.Precedence)
            // {
            //     target[kvp.Key] = kvp.Value; // Keep higher precedence
            // }
        }
    }

    // Original load methods remain the same
    private static Dictionary<SyntaxKind, SyntaxType> LoadUnaryOperators()
    {
        var unaryOperators = new Dictionary<SyntaxKind, SyntaxType>();

        unaryOperators.Add(SyntaxKind.PlusToken, new SyntaxType(SyntaxKind.PlusToken, "+", 6));
        unaryOperators.Add(SyntaxKind.MinusToken, new SyntaxType(SyntaxKind.MinusToken, "-", 6));
        unaryOperators.Add(SyntaxKind.BangToken, new SyntaxType(SyntaxKind.BangToken, "!", 6));
        unaryOperators.Add(SyntaxKind.TildeToken, new SyntaxType(SyntaxKind.TildeToken, "~", 6));

        return unaryOperators;
    }

    private static Dictionary<SyntaxKind, SyntaxType> LoadBinaryOperators()
    {
        var binaryOperators = new Dictionary<SyntaxKind, SyntaxType>();

        binaryOperators.Add(SyntaxKind.StarToken, new SyntaxType(SyntaxKind.StarToken, "*", 5));
        binaryOperators.Add(SyntaxKind.SlashToken, new SyntaxType(SyntaxKind.SlashToken, "/", 5));
        binaryOperators.Add(SyntaxKind.PlusToken, new SyntaxType(SyntaxKind.PlusToken, "+", 4));
        binaryOperators.Add(SyntaxKind.MinusToken, new SyntaxType(SyntaxKind.MinusToken, "-", 4));
        binaryOperators.Add(SyntaxKind.EqualsEqualsToken, new SyntaxType(SyntaxKind.EqualsEqualsToken, "==", 3));
        binaryOperators.Add(SyntaxKind.BangEqualsToken, new SyntaxType(SyntaxKind.BangEqualsToken, "!=", 3));
        binaryOperators.Add(SyntaxKind.GreaterOrEqualsToken, new SyntaxType(SyntaxKind.GreaterOrEqualsToken, ">=", 3));
        binaryOperators.Add(SyntaxKind.GreaterToken, new SyntaxType(SyntaxKind.GreaterToken, ">", 3));
        binaryOperators.Add(SyntaxKind.LessOrEqualsToken, new SyntaxType(SyntaxKind.LessOrEqualsToken, "<=", 3));
        binaryOperators.Add(SyntaxKind.LessToken, new SyntaxType(SyntaxKind.LessToken, "<", 3));
        binaryOperators.Add(SyntaxKind.AmpersandAmpersandToken, new SyntaxType(SyntaxKind.AmpersandAmpersandToken, "&&", 2));
        binaryOperators.Add(SyntaxKind.AmpersandToken, new SyntaxType(SyntaxKind.AmpersandToken, "&", 2));
        binaryOperators.Add(SyntaxKind.PipeToken, new SyntaxType(SyntaxKind.PipeToken, "|", 1));
        binaryOperators.Add(SyntaxKind.PipePipeToken, new SyntaxType(SyntaxKind.PipePipeToken, "||", 1));
        binaryOperators.Add(SyntaxKind.HatToken, new SyntaxType(SyntaxKind.HatToken, "^", 1));
        binaryOperators.Add(SyntaxKind.EqualsToken, new SyntaxType(SyntaxKind.EqualsToken, "=", 0));

        return binaryOperators;
    }

    private static Dictionary<SyntaxKind, SyntaxType> LoadBooleanKeywords()
    {
        var booleanKeywords = new Dictionary<SyntaxKind, SyntaxType>();
        booleanKeywords.Add(SyntaxKind.TrueKeyword, new SyntaxType(SyntaxKind.TrueKeyword, "true", 0));
        booleanKeywords.Add(SyntaxKind.FalseKeyword, new SyntaxType(SyntaxKind.FalseKeyword, "false", 0));
        return booleanKeywords;
    }

    private static Dictionary<SyntaxKind, SyntaxType> LoadScopeKeywords()
    {
        var scopeKeywords = new Dictionary<SyntaxKind, SyntaxType>();
        scopeKeywords.Add(SyntaxKind.OpenParenthesisToken, new SyntaxType(SyntaxKind.OpenParenthesisToken, "(", 0));
        scopeKeywords.Add(SyntaxKind.CloseParenthesisToken, new SyntaxType(SyntaxKind.CloseParenthesisToken, ")", 0));
        scopeKeywords.Add(SyntaxKind.OpenBraceToken, new SyntaxType(SyntaxKind.OpenBraceToken, "{", 0));
        scopeKeywords.Add(SyntaxKind.CloseBraceToken, new SyntaxType(SyntaxKind.CloseBraceToken, "}", 0));
        return scopeKeywords;
    }

    private static Dictionary<SyntaxKind, SyntaxType> LoadStatementKeywords()
    {
        var statementKeywords = new Dictionary<SyntaxKind, SyntaxType>();
        statementKeywords.Add(SyntaxKind.IfKeyword, new SyntaxType(SyntaxKind.IfKeyword, "if", 0));
        statementKeywords.Add(SyntaxKind.ElseKeyword, new SyntaxType(SyntaxKind.ElseKeyword, "else", 0));
        statementKeywords.Add(SyntaxKind.WhileKeyword, new SyntaxType(SyntaxKind.WhileKeyword, "while", 0));
        statementKeywords.Add(SyntaxKind.ForKeyword, new SyntaxType(SyntaxKind.ForKeyword, "for", 0));
        statementKeywords.Add(SyntaxKind.ToKeyword, new SyntaxType(SyntaxKind.ToKeyword, "to", 0));
        return statementKeywords;
    }

    private static Dictionary<SyntaxKind, SyntaxType> LoadVariableKeywords()
    {
        var variableKeywords = new Dictionary<SyntaxKind, SyntaxType>();
        variableKeywords.Add(SyntaxKind.LetKeyword, new SyntaxType(SyntaxKind.LetKeyword, "let", 0));
        variableKeywords.Add(SyntaxKind.VarKeyword, new SyntaxType(SyntaxKind.VarKeyword, "var", 0));
        return variableKeywords;
    }
}