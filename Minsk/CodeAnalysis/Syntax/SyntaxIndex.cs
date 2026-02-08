using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Syntax.Object;
using System.Collections.Generic;
using System.Linq;

namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class SyntaxIndex
    {
        // The single instance (lazy-loaded)
        private static readonly Lazy<SyntaxIndex> _instance = new(() => new SyntaxIndex());

        public static SyntaxIndex Instance => _instance.Value;

        // Instance fields (moved from static)
        private readonly Dictionary<SyntaxKind, SyntaxType> _unaryOperators;
        private readonly Dictionary<SyntaxKind, SyntaxType> _binaryOperators;
        private readonly Dictionary<SyntaxKind, SyntaxType> _assignmentOperators;
        private readonly Dictionary<SyntaxKind, SyntaxType> _reservedKeywords;


        private readonly Dictionary<string, SyntaxKind> _textToKind;
        private readonly Dictionary<string, SyntaxKind> _tokenIndex;

        private readonly Dictionary<SyntaxKind, string> _syntaxKindIndex;
        

        // Private constructor — called only once
        private SyntaxIndex()
        {
            _unaryOperators = LoadUnaryOperators();
            _binaryOperators = LoadBinaryOperators();
            _assignmentOperators = LoadAssignmentOperators();
            _reservedKeywords = LoadReservedKeywords();
            _syntaxKindIndex = BuildSyntaxKindIndex();
            _tokenIndex = BuildOperatorIndex();

            _textToKind = _reservedKeywords.ToDictionary(
                kvp => kvp.Value.Text, kvp => kvp.Key);
        }

        // Public instance properties 
        public Dictionary<SyntaxKind, SyntaxType> UnaryOperators => _unaryOperators;
        public Dictionary<SyntaxKind, SyntaxType> BinaryOperators => _binaryOperators;
        public Dictionary<SyntaxKind, SyntaxType> AssignmentOperators => _assignmentOperators;
        public Dictionary<SyntaxKind, SyntaxType> ReservedKeywords => _reservedKeywords;


        // Indexes properties
        public Dictionary<SyntaxKind, string> SyntaxKindIndex => _syntaxKindIndex;
        public Dictionary<string, SyntaxKind> TokenIndex => _tokenIndex;
        public Dictionary<string, SyntaxKind> TextToKind => _textToKind;

        // All loading methods remain unchanged — just private now
        private Dictionary<SyntaxKind, SyntaxType> LoadUnaryOperators()
        {
            var unaryOperators = new Dictionary<SyntaxKind, SyntaxType>();
            unaryOperators.Add(SyntaxKind.PlusToken, new SyntaxType(SyntaxKind.PlusToken, "+", 6));
            unaryOperators.Add(SyntaxKind.MinusToken, new SyntaxType(SyntaxKind.MinusToken, "-", 6));
            unaryOperators.Add(SyntaxKind.BangToken, new SyntaxType(SyntaxKind.BangToken, "!", 6));
            unaryOperators.Add(SyntaxKind.TildeToken, new SyntaxType(SyntaxKind.TildeToken, "~", 6));
            return unaryOperators;
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadBinaryOperators()
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
            return binaryOperators;
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadReservedKeywords()
        {
            return LoadBooleanKeywords()
                .Concat(LoadStatementKeywords())
                .Concat(LoadVariableKeywords())
                .Concat(LoadScopeKeywords())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadAssignmentOperators()
        {
            var assignmentOperators = new Dictionary<SyntaxKind, SyntaxType>();
            assignmentOperators.Add(SyntaxKind.EqualsToken, new SyntaxType(SyntaxKind.EqualsToken, "=", 0));
            return assignmentOperators;
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadBooleanKeywords()
        {
            var booleanKeywords = new Dictionary<SyntaxKind, SyntaxType>();
            booleanKeywords.Add(SyntaxKind.TrueKeyword, new SyntaxType(SyntaxKind.TrueKeyword, "true", 0));
            booleanKeywords.Add(SyntaxKind.FalseKeyword, new SyntaxType(SyntaxKind.FalseKeyword, "false", 0));
            return booleanKeywords;
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadScopeKeywords()
        {
            var scopeKeywords = new Dictionary<SyntaxKind, SyntaxType>();
            scopeKeywords.Add(SyntaxKind.OpenParenthesisToken, new SyntaxType(SyntaxKind.OpenParenthesisToken, "(", 0));
            scopeKeywords.Add(SyntaxKind.CloseParenthesisToken, new SyntaxType(SyntaxKind.CloseParenthesisToken, ")", 0));
            scopeKeywords.Add(SyntaxKind.OpenBraceToken, new SyntaxType(SyntaxKind.OpenBraceToken, "{", 0));
            scopeKeywords.Add(SyntaxKind.CloseBraceToken, new SyntaxType(SyntaxKind.CloseBraceToken, "}", 0));
            return scopeKeywords;
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadStatementKeywords()
        {
            var statementKeywords = new Dictionary<SyntaxKind, SyntaxType>();
            statementKeywords.Add(SyntaxKind.IfKeyword, new SyntaxType(SyntaxKind.IfKeyword, "if", 0));
            statementKeywords.Add(SyntaxKind.ElseKeyword, new SyntaxType(SyntaxKind.ElseKeyword, "else", 0));
            statementKeywords.Add(SyntaxKind.WhileKeyword, new SyntaxType(SyntaxKind.WhileKeyword, "while", 0));
            statementKeywords.Add(SyntaxKind.ForKeyword, new SyntaxType(SyntaxKind.ForKeyword, "for", 0));
            statementKeywords.Add(SyntaxKind.ToKeyword, new SyntaxType(SyntaxKind.ToKeyword, "to", 0));
            return statementKeywords;
        }

        private Dictionary<SyntaxKind, SyntaxType> LoadVariableKeywords()
        {
            var variableKeywords = new Dictionary<SyntaxKind, SyntaxType>();
            variableKeywords.Add(SyntaxKind.LetKeyword, new SyntaxType(SyntaxKind.LetKeyword, "let", 0));
            variableKeywords.Add(SyntaxKind.VarKeyword, new SyntaxType(SyntaxKind.VarKeyword, "var", 0));
            return variableKeywords;
        }

        private Dictionary<SyntaxKind, string> BuildSyntaxKindIndex()
        {
            var combined = new Dictionary<SyntaxKind, string>();
            MergeDictionary(combined, _unaryOperators);
            MergeDictionary(combined, _binaryOperators);
            MergeDictionary(combined, _assignmentOperators);
            MergeDictionary(combined, _reservedKeywords);
            return combined;
        }

        private Dictionary<string, SyntaxKind> BuildOperatorIndex()
        {
            var combined = new Dictionary<SyntaxKind, string>();
            MergeDictionary(combined, _unaryOperators);
            MergeDictionary(combined, _binaryOperators);
            MergeDictionary(combined, _assignmentOperators);
            MergeDictionary(combined, LoadScopeKeywords());
            return combined.ToDictionary(
                kvp => kvp.Value, kvp => kvp.Key); ;
        }

        private void MergeDictionary(
            Dictionary<SyntaxKind, string> target,
            Dictionary<SyntaxKind, SyntaxType> source)
        {
            foreach (var kvp in source)
            {
                if (!target.ContainsKey(kvp.Key))
                {
                    target.Add(kvp.Key, kvp.Value.Text);
                }
            }
        }

        // Public accessors now use instance fields
        public Dictionary<SyntaxKind, SyntaxType> GetUnaryOperators() => _unaryOperators;
        public Dictionary<SyntaxKind, SyntaxType> GetBinaryOperators() => _binaryOperators;
        public Dictionary<SyntaxKind, SyntaxType> GetAssignmentOperators() => _assignmentOperators;
        public Dictionary<SyntaxKind, SyntaxType> GetReservedKeywords() => _reservedKeywords;
        public Dictionary<SyntaxKind, string> GetSyntaxKindIndex() => _syntaxKindIndex;
    }
}