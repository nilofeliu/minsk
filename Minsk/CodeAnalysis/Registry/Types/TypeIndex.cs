namespace Minsk.CodeAnalysis.Registry.Types;

internal static class TypeIndex
{
    internal static Type GetClrTypeFromText(string text)
    {
        return text switch
        {
            "char" => typeof(char),
            "bool" => typeof(bool),
            "byte" => typeof(byte),
            "sbyte" => typeof(sbyte),
            "short" => typeof(short),
            "ushort" => typeof(ushort),
            "int" => typeof(int),
            "uint" => typeof(uint),
            "long" => typeof(long),
            "ulong" => typeof(ulong),
            "float" => typeof(float),
            "double" => typeof(double),
            "decimal" => typeof(decimal),
            "string" => typeof(string),
            "object" => typeof(object),
            "void" => typeof(void),
            "single" => typeof(float),
            "int8" => typeof(sbyte),
            "int16" => typeof(short),
            "int32" => typeof(int),
            "int64" => typeof(long),
            "uint8" => typeof(byte),
            "uint16" => typeof(ushort),
            "uint32" => typeof(uint),
            "uint64" => typeof(ulong),
            _ => throw new ArgumentException($"Unknown type: {text}")
        };
    }
}