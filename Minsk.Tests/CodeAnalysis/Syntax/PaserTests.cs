using Minsk.CodeAnalysis.Syntax;

namespace Minsk.Tests.CodeAnalysis.Syntax;

public class PaserTests
{
    [Fact]
    public void Parser_Parses_BinaryExpression()
    {
        var text = "1 + 2";
        var syntaxTree = SyntaxTree.Parse(text);
        var expression = syntaxTree.Root as ExpressionSyntax;
        var binaryExpression = Assert.IsType<BinaryExpressionSyntax>(expression);
        Assert.IsType<NumberExpressionSyntax>(binaryExpression.Left);
        Assert.Equal(SyntaxKind.PlusToken, binaryExpression.OperatorToken.Kind);
        Assert.IsType<NumberExpressionSyntax>(binaryExpression.Right);
    }
}
