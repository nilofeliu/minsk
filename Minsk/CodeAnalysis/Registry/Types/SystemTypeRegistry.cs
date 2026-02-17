using Minsk.CodeAnalysis.Registry.Types;
using System.Collections;

internal class SystemTypeRegistry
{
    internal class TypeSymbolRegistry : IEnumerable<KeyValuePair<string, PrimitiveTypeObject>>
    {
        private readonly Dictionary<string, PrimitiveTypeObject> _data = new Dictionary<string, PrimitiveTypeObject>();

        public bool TryAdd(string key, PrimitiveTypeObject symbol)
        {
            if (_data.ContainsKey(key))
                return false;
            _data.Add(key, symbol);
            return true;
        }

        public bool TryGetValue(string name, out PrimitiveTypeObject symbol)
        {
            return _data.TryGetValue(name, out symbol);
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }


        public IEnumerator<KeyValuePair<string, PrimitiveTypeObject>> GetEnumerator()
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

    public PrimitiveTypeObject? GetSymbolByCategory(string categoryName)
    {
        return _tables.TryGetValue(categoryName, out var symbol) ? symbol : null;
    }

    public List<PrimitiveTypeObject> GetAllSymbols()
    {
        return _tables.ToList().Select(kvp => kvp.Value).ToList();
    }

    public void Register(string categoryName, PrimitiveTypeObject symbol)
    {
        _tables.TryAdd(categoryName, symbol);
    }

    public bool HasCategory(string categoryName)
    {
        return _tables.ContainsKey(categoryName);
    }
}