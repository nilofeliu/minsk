using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minsk.helper
{
    internal class TerminalPrompt
    {
        internal static void PromptReader()
        {
            bool showTree = false;
            bool showLexicon = false;

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
                        case "#showtokens":
                        showLexicon = !showLexicon;
                        Console.WriteLine(showLexicon ? "Showing lexical tokens." : "Not showing lexical tokens.");
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
                var binder = new Binder();
                var boundExpression = binder.BindExpression((ExpressionSyntax)syntaxTree.Root);

                IReadOnlyList<string> diagnostics = syntaxTree.Diagnostics;

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrintTree(syntaxTree.Root);
                    Console.ResetColor();
                }
                if (showLexicon)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrintTokens(new Lexer(line));
                    Console.ResetColor();
                }
                                
                if (!diagnostics.Any())
                {
                    var e = new Evaluator(boundExpression);
                    var result = e.Evaluate();
                    Console.WriteLine($"Result: {result}");
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }
                    Console.ResetColor();
                }
            }
        }
        private static void PrintTokens(Lexer lexer)
        {

            List<SyntaxToken> tokens = new();

            Console.WriteLine("-----------------------");
            while (true)
            {
                var token = lexer.Lex();
                if (token.Kind == SyntaxKind.EndOfFileToken)
                    break;
                tokens.Add(token);
            }

            int position = 0;

            foreach (var token in tokens)
            {
                string padding = GetPadding(position);

                Console.Write($"Position: {position} {padding}Token: {token.Kind} '{token.Text}' ");
                if (token.Value != null)
                    Console.Write($"Value: {token.Value}");
                Console.WriteLine(' ');
                position++;
            }
        }

        private static string GetPadding(int position)
        {
            int digitCount = position.ToString().Length;
            return new string(' ', 4 - digitCount);
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
