using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.System.Types
{
    internal static class SystemPrimitiveTypesRepository
    {
        //This is for TypeKind Index. Not SyntaxKind. Need to refactor it.
        internal static List<PrimitiveTypeObject> LoadTypeKeywords()
        {
            var typeKeywords = new List<PrimitiveTypeObject>();

            // Built-in value types
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.CharKeyword, "char"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.BoolKeyword, "bool"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.ByteKeyword, "byte"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.SByteKeyword, "sbyte"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.ShortKeyword, "short"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.UShortKeyword, "ushort"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.IntKeyword, "int"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.UIntKeyword, "uint"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.LongKeyword, "long"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.ULongKeyword, "ulong"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.FloatKeyword, "float"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.DoubleKeyword, "double"));
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.DecimalKeyword, "decimal"));

            // Built-in reference types
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.StringKeyword, "string"));
            //typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.ObjectKeyword, "object"));

            // Special types
            typeKeywords.Add(new PrimitiveTypeObject(SyntaxKind.VoidKeyword, "void"));

            return typeKeywords;
        }
    }
}
