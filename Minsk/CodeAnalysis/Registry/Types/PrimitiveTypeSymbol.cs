using Minsk.CodeAnalysis.Syntax.Kind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Registry.Types;

public sealed class PrimitiveTypeSymbol
{
    public string Text { get; }
    public SyntaxKind ExpectedKind => SyntaxKind.LiteralExpression; // This might need to be configurable
    public string Namespace { get; }
    public Type ClrType { get; }
    public bool IsValueType { get; }
    public bool IsReferenceType { get; }
    public bool IsBuiltIn { get; }
    public int? Size { get; } // Make nullable since reference types don't have fixed size

    public PrimitiveTypeSymbol(string text, string @namespace = "", Type clrType = null, bool isBuiltIn = true)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Namespace = @namespace ?? "";

        if (clrType == null)
        {
            ClrType = TypeIndex.GetClrTypeFromText(text)
            ?? throw new ArgumentException($"Unknown type: {text}", nameof(text));
        }
        else
        {
            ClrType = clrType;
        }
        
        IsBuiltIn = isBuiltIn;
        IsValueType = ClrType.IsValueType;
        IsReferenceType = !ClrType.IsValueType;

        // Only get size for value types
        if (IsValueType)
        {
            try
            {
                Size = Marshal.SizeOf(ClrType);
            }
            catch
            {
                Size = null; // Fallback if size can't be determined
            }
        }
        else
        {
            Size = null; // Reference types don't have fixed size
        }
    }
}

