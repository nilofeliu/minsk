using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minsk
{
    internal abstract class Repl
    {
        private readonly StringBuilder _textBuilder = new();

        internal void Run()
        {

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (_textBuilder.Length == 0)
                    Console.Write("» ");
                else
                    Console.Write("| ");
                Console.ResetColor();

                string input = Console.ReadLine();
                var isBlank = string.IsNullOrWhiteSpace(input);
                                
                if (_textBuilder.Length == 0)
                {
                    if (isBlank)
                    {
                        break;
                    }
                    else if (input.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                    {
                        EvaluateMetaCommand(input);
                        continue;
                    }
                }

                _textBuilder.AppendLine(input);
                var text = _textBuilder.ToString();
                if (!IsCompletedSubmission(text))
                    continue;

                EvaluateSubmission(text);

                _textBuilder.Clear();
            }
        }

        protected virtual void EvaluateMetaCommand(string input)
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine($"Invalid command {input}");
            Console.ResetColor();
        }

        protected abstract bool IsCompletedSubmission(string text);

        protected abstract void EvaluateSubmission(string text);

        protected static void PrintExceptions(List<Exception> exceptions)
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

        protected static void PrintCommands()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("#exit - Exit the application");
            Console.WriteLine("#showtree - Toggle showing parse trees");
            Console.WriteLine("#showprogram - Toggle showing bound tree");
            Console.WriteLine("#cls - Clear the console");
            Console.WriteLine("#help - Show this help message");
            Console.WriteLine("Type any expression to evaluate it.");
        }


    }
}
