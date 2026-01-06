using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk
{
    internal class TerminalPrompt
    {
        internal static void PromptReader()
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
                else if (line.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                {
                    switch (line.ToLower())
                    {
                        case "#exit":
                        Console.WriteLine("Exiting...");
                        return;
                        case "#showtree":
                        showTree = !showTree;
                        Console.WriteLine(showTree ? $"Showing parse trees." : "Not showing parse trees.");
                        continue;
                        case "#cls":
                        Console.Clear();
                        continue;
                        case "#help":
                        PrintCommands();
                        continue;
                        default:
                        Console.WriteLine("Invalid command.");
                        continue;
                    }
                }
                               
                var syntaxTree = SyntaxTree.Parse(line);
                var compilation = new Compilation(syntaxTree);
                var result = compilation.Evaluate();

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrintTree(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!result.Diagnostics.Any())
                {
                    Console.WriteLine($"{result.Value}");
                }
                else
                {
                    Console.WriteLine();
                  
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();

                        var prefix = line.Substring(0, diagnostic.Span.Start);
                        var error = line.Substring(diagnostic.Span.Start, diagnostic.Span.Length);
                        var suffix = line.Substring(diagnostic.Span.End);

                        Console.Write("    ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();

                        Console.WriteLine(suffix);

                        Console.WriteLine();

                    }
                    
                }
            }
        }
        
        private static void PrintCommands()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("#exit - Exit the application");
            Console.WriteLine("#showtree - Toggle showing parse trees");
            Console.WriteLine("#showtokens - Toggle showing lexical tokens");
            Console.WriteLine("#cls - Clear the console");
            Console.WriteLine("#help - Show this help message");
            Console.WriteLine("Type any expression to evaluate it.");
        }

        private static void PrintTree(SyntaxNode node, string indent = "", bool isLast = true)
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

            indent += isLast ? "   " : "│  ";


            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
            {
                PrintTree(child, indent, child == lastChild);
            }

        }
    }
}
