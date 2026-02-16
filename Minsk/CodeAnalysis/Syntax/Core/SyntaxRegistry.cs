using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Syntax.Core;

internal class SyntaxRegistry
{
    internal class SymbolRegistry : IEnumerable<KeyValuePair<SymbolTable, List<SyntaxSymbol>>>
    {
        private readonly Dictionary<SymbolTable, List<SyntaxSymbol>> _data = new();

        public bool TryAdd(SymbolTable table, List<SyntaxSymbol> symbols)
        {
            if (_data.ContainsKey(table))
                return false;
            _data.Add(table, symbols);
            return true;
        }
        public bool TryGetValue(SymbolTable categoryName, out List<SyntaxSymbol> existingList)
        {
            return _data.TryGetValue(categoryName, out existingList);
        }
        public bool ContainsKey(SymbolTable table)
        {
            return _data.ContainsKey(table);
        }

        IEnumerator<KeyValuePair<SymbolTable, List<SyntaxSymbol>>> IEnumerable<KeyValuePair<SymbolTable, List<SyntaxSymbol>>>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }

    private static readonly Lazy<SyntaxRegistry> _instance = new(() => new SyntaxRegistry());

    public static SyntaxRegistry Instance => _instance.Value;

    private readonly SymbolRegistry _tables;

    private readonly Dictionary<string, SyntaxKind> _kindIndex = new();
    private readonly Dictionary<SyntaxKind, string> _textIndex = new ();

    // Indices properties
    internal Dictionary<SyntaxKind, string> TextIndex => _textIndex;
    internal Dictionary<string, SyntaxKind> KindIndex => _kindIndex;

    internal SymbolRegistry GetSymbolRegistry => _tables;

    private SyntaxRegistry()
    {
        _tables = new SymbolRegistry();
        _tables.TryAdd(SymbolTable.Operators, new List<SyntaxSymbol>());
        _tables.TryAdd(SymbolTable.Keywords, new List<SyntaxSymbol>());
        _tables.TryAdd(SymbolTable.Delimiters, new List<SyntaxSymbol>());
    }

    private void UpdateIndexTable(SyntaxSymbol symbol)
    {
        UpdateKindIndex(symbol);
        UpdateTextIndex(symbol);
    }

    private void UpdateKindIndex(SyntaxSymbol symbol)
    {
        if (_kindIndex.ContainsKey(symbol.Text))
            return;

        _kindIndex.Add(symbol.Text, symbol.Kind);
    }
    private void UpdateTextIndex(SyntaxSymbol symbol)
    {
        if (_textIndex.ContainsKey(symbol.Kind))
            return;

        _textIndex.Add(symbol.Kind, symbol.Text);
    }

    public List<SyntaxSymbol> GetSymbolsByCategory(SymbolTable table)
    {
        return _tables.TryGetValue(table, out var list) ? list : new List<SyntaxSymbol>();
    }
    public List<SyntaxSymbol> GetAllSymbols()
    {
        var allSymbols = new List<SyntaxSymbol>();
        foreach (var kvp in _tables)
        {
            allSymbols.AddRange(kvp.Value);
        }
        return allSymbols;
    }

    public void Register(SymbolTable categoryName, SyntaxSymbol symbol)
    {
        if (_tables.TryGetValue(categoryName, out var existingList))
        {
            existingList.Add(symbol);
        }
        else
        {
            var newList = new List<SyntaxSymbol>();
            newList.Add(symbol);
            _tables.TryAdd(categoryName, newList);
        }
        UpdateIndexTable(symbol);
    }

    public void Register(SymbolTable categoryName, List<SyntaxSymbol> symbolList)
    {
        if (!_tables.TryGetValue(categoryName, out var list))
        {
            list = new List<SyntaxSymbol>();
            _tables.TryAdd(categoryName, list);
        }
        list.AddRange(symbolList);

        foreach (var symbol in symbolList)
        {
            UpdateIndexTable(symbol);
        }
    }

    public List<SyntaxSymbol> GetCategoryTable(SymbolTable categoryName)
    {
        return _tables.TryGetValue(categoryName, out var index) ? index : null;
    }

    public bool HasCategory(SymbolTable categoryName)
    {
        return _tables.ContainsKey(categoryName);
    }
}

public enum SymbolTable
{
    Operators,
    Keywords,
    Delimiters,
}