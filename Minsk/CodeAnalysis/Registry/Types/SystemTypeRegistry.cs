using Minsk.CodeAnalysis.Registry.Types;
using System.Collections;

internal class SystemTypeRegistry
{
    internal class TypeSymbolRegistry : IEnumerable<KeyValuePair<string, PrimitiveTypeSymbol>>
    {
        private readonly Dictionary<string, PrimitiveTypeSymbol> _data = new Dictionary<string, PrimitiveTypeSymbol>();

        public bool TryAdd(string key, PrimitiveTypeSymbol symbol)
        {
            if (_data.ContainsKey(key))
                return false;
            _data.Add(key, symbol);
            return true;
        }

        public bool TryGetValue(string name, out PrimitiveTypeSymbol symbol)
        {
            return _data.TryGetValue(name, out symbol);
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }


        public IEnumerator<KeyValuePair<string, PrimitiveTypeSymbol>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private static readonly Lazy<SystemTypeRegistry> _instance = new(() => new SystemTypeRegistry());
    public static SystemTypeRegistry Instance => _instance.Value;

    private readonly TypeSymbolRegistry _tables;

    internal TypeSymbolRegistry GetSymbolRegistry => _tables;

    private SystemTypeRegistry()
    {
        _tables = new TypeSymbolRegistry();
    }

    public PrimitiveTypeSymbol? GetSymbolByCategory(string categoryName)
    {
        return _tables.TryGetValue(categoryName, out var symbol) ? symbol : null;
    }

    public List<PrimitiveTypeSymbol> GetAllSymbols()
    {
        return _tables.ToList().Select(kvp => kvp.Value).ToList();
    }

    public void Register(string categoryName, PrimitiveTypeSymbol symbol)
    {
        _tables.TryAdd(categoryName, symbol);
    }

    public bool HasCategory(string categoryName)
    {
        return _tables.ContainsKey(categoryName);
    }
}