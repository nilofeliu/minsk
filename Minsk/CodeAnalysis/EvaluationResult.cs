using System.Collections.Immutable;

namespace Minsk.CodeAnalysis
{
    public sealed class EvaluationResult
    {
        public EvaluationResult(ImmutableArray<Diagnostic> diagnostics, object value)
        {
            Value = value;
            Diagnostics = diagnostics;
        }
        public object Value { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
    }
}
