using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Expression;
using static System.Net.Mime.MediaTypeNames;

namespace Minsk.CodeAnalysis.Syntax.Core;

public static class SyntaxFacts
{
    public static bool MatchExpressionType(ExpressionSyntax expr1, ExpressionSyntax expr2)
    {
        var output = false;
        var pattern1 = expr1.GetType();
        var pattern2 = expr2.GetType();

        if (pattern1 == pattern2) { output = true; }

        return output;
    }


    
}
