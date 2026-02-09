using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis.Text;
using System.Text;

namespace Minsk
{
    internal sealed class MinskRepl : Repl
    {
        private Compilation _previous;

        private bool _showTree;
        private bool _showProgram;
        private readonly Dictionary<VariableSymbol, object> _variables = new();

        protected override void EvaluateMetaCommand(string input)
        {
            switch (input.ToLower().Trim())
            {
                case "#exit":
                    Console.WriteLine("Exiting...");
                    return;
                case "#showtree":
                    _showTree = !_showTree;
                    Console.WriteLine(_showTree ? $"Showing parse trees." : "Not showing parse trees.");
                    break;
                case "#showprogram":
                    _showProgram = !_showProgram;
                    Console.WriteLine(_showProgram ? $"Showing bound tree." : "Not showing bound tree.");
                    break;
                case "#cls":
                    Console.Clear();
                    break;
                case "#reset":
                    _previous = null;
                    break;
                case "#help":
                    PrintCommands();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid command {input}.");
                    Console.ResetColor();
                    break;
            }
        }

        protected override bool IsCompletedSubmission(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            var syntaxTree = SyntaxTree.Parse(text);

            if (syntaxTree.Diagnostics.Any())
                return false;

            return true;
        }

        protected override void EvaluateSubmission(string text)
        {
            var syntaxTree = SyntaxTree.Parse(text);

            var compilation = _previous == null
                ? new Compilation(syntaxTree)
                : _previous.ContinueWith(syntaxTree);


            if (_showTree)
                syntaxTree.Root.WriteTo(Console.Out);

            if (_showProgram)
                compilation.EmitTree(Console.Out);

            var result = compilation.Evaluate(_variables);

            if (!result.Diagnostics.Any())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{result.Value}");
                Console.ResetColor();

                _previous = compilation;
            }
            else
            {
                Console.WriteLine();
                List<Exception> exceptions = new();

                foreach (var diagnostic in result.Diagnostics)
                {
                    var lineIndex = syntaxTree.Text.GetLineIndex(diagnostic.Span.Start);
                    var line = syntaxTree.Text.Lines[lineIndex];
                    var lineNumber = lineIndex + 1;
                    var character = diagnostic.Span.Start - line.Start + 1;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"({lineNumber}, {character}): ");
                    Console.WriteLine(diagnostic);
                    Console.ResetColor();

                    try
                    {

                        var prefixSpan = TextSpan.FromBounds(line.Start, diagnostic.Span.Start);
                        var suffixSpan = TextSpan.FromBounds(diagnostic.Span.End, line.End);

                        var prefix = syntaxTree.Text.ToString(prefixSpan);
                        var error = syntaxTree.Text.ToString(diagnostic.Span);
                        var suffix = syntaxTree.Text.ToString(suffixSpan);

                        Console.Write("    ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();

                        Console.WriteLine(suffix);

                        Console.WriteLine();

                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
                PrintExceptions(exceptions);
            }
        }


    }

}
