namespace Minsk.CodeAnalysis
{
    public sealed class Diagnostic
    {
        public Diagnostic(TextSpan span, string message)
        {
            Message = message;
            Span = span;
        }
        public string Message { get; }
        public TextSpan Span { get; }

        public override string ToString() => Message;
    }
}
