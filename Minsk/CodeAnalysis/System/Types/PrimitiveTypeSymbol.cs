using Minsk.CodeAnalysis.Syntax.Kind;
using System.Runtime.InteropServices;

namespace Minsk.CodeAnalysis.System.Types
{
    public sealed class PrimitiveTypeObject
    {
        public SyntaxKind Kind { get; }  // <-- SyntaxKind, not ObjectTypeKind
        public string Text { get; }
        public SyntaxKind ExpectedSyntaxKind => SyntaxKind.LiteralExpression;
        public Type ClrType { get; }
        public bool IsValueType { get; }
        public bool IsReferenceType { get; }
        public bool IsBuiltIn { get; }
        public int? Size { get; }

        public PrimitiveTypeObject(SyntaxKind kind, string text, Type clrType = null, bool isBuiltIn = true)
        {
            Kind = kind;
            Text = text ?? throw new ArgumentNullException(nameof(text));

            ClrType = clrType ?? TypeIndex.GetClrTypeFromText(text)
                ?? throw new ArgumentException($"Unknown type: {text}", nameof(text));

            IsBuiltIn = isBuiltIn;
            IsValueType = ClrType.IsValueType;
            IsReferenceType = !ClrType.IsValueType;

            if (IsValueType)
            {
                try
                {
                    Size = Marshal.SizeOf(ClrType);
                }
                catch
                {
                    Size = null;
                }
            }
            else
            {
                Size = null;
            }
        }
    }
}