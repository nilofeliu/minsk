namespace Minsk.CodeAnalysis
{
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        abstract public IEnumerable<SyntaxNode> GetChildren();

    }
}
