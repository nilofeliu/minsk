using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Binding.Expressions;
using Minsk.CodeAnalysis.Binding.Objects;
using Minsk.CodeAnalysis.Binding.Statements;
using Minsk.CodeAnalysis.Symbols;
using Minsk.CodeAnalysis.Syntax.Kind;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.CodeAnalysis.Lowering;

internal sealed class Lowerer : BoundTreeRewrite
{

    private int _labelCount;
    
    private Lowerer()
    {
    }

    internal LabelSymbol GenerateLabel()
    {
        var name = $"Label{++_labelCount}";
        return new LabelSymbol(name);
    }

    public static BoundBlockStatement Lower(BoundStatement statement)
    {
        var lowerer = new Lowerer();
        var result = lowerer.RewriteStatement(statement);
        return Flatten(result);
    }

    private static BoundBlockStatement Flatten(BoundStatement statement)
    {
        var builder = ImmutableArray.CreateBuilder<BoundStatement>();
        var stack = new Stack<BoundStatement>();
        stack.Push(statement);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current is BoundBlockStatement block)
            {
                foreach (var s in block.Statements.Reverse())
                    stack.Push(s);
            }
            else
            {
                builder.Add(current);
            }
        }

        return new BoundBlockStatement(builder.ToImmutable());
    }

    protected override BoundStatement RewriteIfStatement(BoundIfStatement node)
    {
        return IfStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteWhileStatement(BoundWhileStatement node)
    {
        return WhileStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteForStatement(BoundForStatement node)
    {
        return ForStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteSwitchStatement(BoundSwitchStatement node)
    {
        return SwitchStatement.Rewrite(this, node);
    }

}

