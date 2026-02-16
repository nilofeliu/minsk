using Minsk.CodeAnalysis.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Registry.Types
{
    internal static class SystemPrimitiveTypesRepository
    {
        //This is for TypeKind Index. Not SyntaxKind. Need to refactor it.
        internal static List<PrimitiveTypeSymbol> LoadTypeKeywords()
        {
            var typeKeywords = new List<PrimitiveTypeSymbol>();

            // Built-in value types
            typeKeywords.Add(new PrimitiveTypeSymbol("char", "System.Char"));
            typeKeywords.Add(new PrimitiveTypeSymbol("bool", "System.Boolean"));
            typeKeywords.Add(new PrimitiveTypeSymbol("byte", "System.Byte"));
            typeKeywords.Add(new PrimitiveTypeSymbol("sbyte", "System.SByte"));
            typeKeywords.Add(new PrimitiveTypeSymbol("short", "System.Int16"));
            typeKeywords.Add(new PrimitiveTypeSymbol("ushort", "System.UInt16"));
            typeKeywords.Add(new PrimitiveTypeSymbol("int", "System.Int32"));
            typeKeywords.Add(new PrimitiveTypeSymbol("uint", "System.UInt32"));
            typeKeywords.Add(new PrimitiveTypeSymbol("long", "System.Int64"));
            typeKeywords.Add(new PrimitiveTypeSymbol("ulong", "System.UInt64"));
            typeKeywords.Add(new PrimitiveTypeSymbol("float", "System.Single"));
            typeKeywords.Add(new PrimitiveTypeSymbol("double", "System.Double"));
            typeKeywords.Add(new PrimitiveTypeSymbol("decimal", "System.Decimal"));

            // Built-in reference types
            typeKeywords.Add(new PrimitiveTypeSymbol("string", "System.String"));
            typeKeywords.Add(new PrimitiveTypeSymbol("object", "System.Object"));

            // Special types
            typeKeywords.Add(new PrimitiveTypeSymbol("void", "System.Void"));

            // .NET Framework type aliases (System.*)
            typeKeywords.Add(new PrimitiveTypeSymbol("single", "System.Single"));
            typeKeywords.Add(new PrimitiveTypeSymbol("int8", "System.SByte"));
            typeKeywords.Add(new PrimitiveTypeSymbol("int16", "System.Int16"));
            typeKeywords.Add(new PrimitiveTypeSymbol("int32", "System.Int32"));
            typeKeywords.Add(new PrimitiveTypeSymbol("int64", "System.Int64"));
            typeKeywords.Add(new PrimitiveTypeSymbol("uint8", "System.Byte"));
            typeKeywords.Add(new PrimitiveTypeSymbol("uint16", "System.UInt16"));
            typeKeywords.Add(new PrimitiveTypeSymbol("uint32", "System.UInt32"));
            typeKeywords.Add(new PrimitiveTypeSymbol("uint64", "System.UInt64"));

            return typeKeywords;
        }
    }
}
