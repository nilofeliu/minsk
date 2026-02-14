using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;
using static Minsk.CodeAnalysis.Syntax.Core.SyntaxDefinitions;

namespace Minsk.CodeAnalysis.Syntax.Core;
    public sealed class SyntaxIndex
    {
        // The single instance (lazy-loaded)
        private static readonly Lazy<SyntaxIndex> _instance = new(() => new SyntaxIndex());

        public static SyntaxIndex Instance => _instance.Value;

        // Instance fields (moved from static)
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _unaryOperators;
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _binaryOperators;
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _assignmentOperators;
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _reservedKeywords;
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _flowControlKeywords;
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _typedVariablesKeywords;
        private readonly Dictionary<SyntaxKind, SyntaxSymbol> _punctuationOperators;


    private readonly Dictionary<string, SyntaxKind> _textToKind;
        private readonly Dictionary<string, SyntaxKind> _tokenIndex;

        private readonly Dictionary<SyntaxKind, string> _syntaxKindIndex;
        

        // Private constructor — called only once
        private SyntaxIndex()
        {
            _unaryOperators = LoadUnaryOperators();
            _binaryOperators = LoadBinaryOperators();
            _assignmentOperators = LoadAssignmentOperators();
            _punctuationOperators = LoadPunctuationOperators();
            _reservedKeywords = LoadReservedKeywords();
            _flowControlKeywords = LoadFlowControlKeywords();
            _typedVariablesKeywords = LoadTypeKeywords();

            _syntaxKindIndex = BuildSyntaxKindIndex();
            _tokenIndex = BuildOperatorIndex();

            _textToKind = _reservedKeywords.ToDictionary(
                kvp => kvp.Value.Text, kvp => kvp.Key);
        }


        // Public instance properties 
        internal Dictionary<SyntaxKind, SyntaxSymbol> UnaryOperators => _unaryOperators;
        internal Dictionary<SyntaxKind, SyntaxSymbol> BinaryOperators => _binaryOperators;
        internal Dictionary<SyntaxKind, SyntaxSymbol> AssignmentOperators => _assignmentOperators;
        internal Dictionary<SyntaxKind, SyntaxSymbol> ReservedKeywords => _reservedKeywords;
        internal Dictionary<SyntaxKind, SyntaxSymbol> FlowControlKeywords => _flowControlKeywords;
        internal Dictionary<SyntaxKind, SyntaxSymbol> TypedVariablesKeywords => _typedVariablesKeywords;


        // Indexes properties
        internal Dictionary<SyntaxKind, string> SyntaxKindIndex => _syntaxKindIndex;
        internal Dictionary<string, SyntaxKind> TokenIndex => _tokenIndex;
        internal Dictionary<string, SyntaxKind> TextToKind => _textToKind;

       
        private Dictionary<SyntaxKind, SyntaxSymbol> LoadReservedKeywords()
        {
            return LoadBooleanKeywords()
                .Concat(LoadStatementKeywords())
                .Concat(LoadVariableKeywords())
                .Concat(LoadScopeKeywords())
                .Concat(LoadTypeKeywords())
                .Concat(LoadFlowControlKeywords()) 
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }


        private Dictionary<SyntaxKind, string> BuildSyntaxKindIndex()
        {
            var combined = new Dictionary<SyntaxKind, string>();
            MergeDictionary(combined, _unaryOperators);
            MergeDictionary(combined, _binaryOperators);
            MergeDictionary(combined, _assignmentOperators);
            MergeDictionary(combined, _reservedKeywords);
            MergeDictionary(combined, _punctuationOperators);
        return combined;
        }

        private Dictionary<string, SyntaxKind> BuildOperatorIndex()
        {
            var combined = _syntaxKindIndex;

            MergeDictionary(combined, LoadScopeKeywords());

            return combined.ToDictionary(
                kvp => kvp.Value, kvp => kvp.Key); ;
        }

        private void MergeDictionary(
            Dictionary<SyntaxKind, string> target,
            Dictionary<SyntaxKind, SyntaxSymbol> source)
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
        public Dictionary<SyntaxKind, SyntaxSymbol> GetUnaryOperators() => _unaryOperators;
        public Dictionary<SyntaxKind, SyntaxSymbol> GetBinaryOperators() => _binaryOperators;
        public Dictionary<SyntaxKind, SyntaxSymbol> GetAssignmentOperators() => _assignmentOperators;
        public Dictionary<SyntaxKind, SyntaxSymbol> GetReservedKeywords() => _reservedKeywords;
        public Dictionary<SyntaxKind, SyntaxSymbol> GetTypedVariablesKeywords() => _typedVariablesKeywords;
        public Dictionary<SyntaxKind, SyntaxSymbol> GetFlowControlKeywords() => _flowControlKeywords;

        public Dictionary<SyntaxKind, string> GetSyntaxKindIndex() => _syntaxKindIndex;
    }
