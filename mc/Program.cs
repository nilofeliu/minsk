using Minsk.CodeAnalysis;
using System;

namespace Minsk
{

    class Program
    {
        static void Main(string[] args)
        {
            LineReader();
        }

        static void LineReader()
        {
            bool showTree = false;

            while (true)
            {
                Console.Write(">");
                string? line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("No Input. Exiting...");
                    return;
                }
                if (line.Equals("#exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
                else if (line.Equals("#showtree", StringComparison.OrdinalIgnoreCase))
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? $"Showing parse trees." : "Not showing parse trees.");
                    continue;
                }
                else if (line.Equals("#cls", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    continue;
                }


                    var syntaxTree = SyntaxTree.Parse(line);

                if (showTree)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = color; 
                }

              //var lexer = new Lexer(line);
              //LexerPrinter(lexer);

                if (!syntaxTree.Diagnostics.Any())
                {
                    var e = new Evaluator((ExpressionSyntax)syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine($"\nResult: {result}");
                }
                else
                {
                    Console.WriteLine();
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }
                    Console.ForegroundColor = color;
                }
            }

            static void LexerPrinter(Lexer lexer)
            {
                Console.WriteLine("===========================================");
                while (true)
                {
                    var token = lexer.NextToken();
                    if (token.Kind == SyntaxKind.EndOfFileToken)
                        break;
                    Console.Write($"{token.Kind} '{token.Text}' ");
                    if (token.Value != null)
                        Console.Write($"{token.Value}");
                    Console.WriteLine(' ');
                }
            }

            static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
            {
                // |__
                // |--
                // |

                var marker = isLast ? "└──" : "├──";

                Console.Write($"{indent}{marker}{node.Kind}");

                if (node is SyntaxToken t && t.Value != null)
                {
                    Console.Write($" ");
                    Console.Write(t.Value);
                }

                Console.WriteLine();

                indent += isLast ? "    " : "│   ";


                var lastChild = node.GetChildren().LastOrDefault();

                foreach (var child in node.GetChildren())
                {
                    PrettyPrint(child, indent, child == lastChild);
                }

            }
        }
    }

}
