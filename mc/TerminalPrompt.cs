using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis.Text;
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
            Dictionary<VariableSymbol, object> variables = new();
            var textBuilder = new StringBuilder();


            while (true)
            {
                if (textBuilder.Length == 0) 
                    Console.Write("> ");
                else
                    Console.Write("| ");

                string input = Console.ReadLine();
                var isBlank = string.IsNullOrWhiteSpace(input);
                    

                if (textBuilder.Length == 0)
                {
                    if (isBlank)
                    {
                        break;
                    }
                    if (input.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (input.ToLower().Trim())
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
                }

                textBuilder.AppendLine(input);
                var text = textBuilder.ToString();

                var syntaxTree = SyntaxTree.Parse(text);

                if (!isBlank && syntaxTree.Diagnostics.Any())
                {
                    continue;
                }

                

                var compilation = new Compilation(syntaxTree);
                var result = compilation.Evaluate(variables);
                
                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    syntaxTree.Root.WriteTo(Console.Out);
                    Console.ResetColor();
                }

                if (!result.Diagnostics.Any())
                {
                    Console.WriteLine($"{result.Value}");
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
                textBuilder.Clear();
            }
        }
        private static void PrintExceptions(List<Exception> exceptions)
        {
            if (exceptions.Count == 0)
                return;
            Console.WriteLine("Exceptions:");
            foreach (var ex in exceptions)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine(ex.ToString());
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
    }
}
