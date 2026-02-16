using Minsk.CodeAnalysis;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Minsk
{
    internal abstract class Repl
    {

        protected Compilation _previous;

        internal void Run()
        {

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                var text = EditSubmissionOld();
                if (text == null) 
                    return; 


                EvaluateSubmission(text);
            }
        }

        private sealed class SubmissionView
        {
            private readonly ObservableCollection<string> _submissionDocument;
            private readonly int _cursorTop;
            private int _renderedLineCount;
            private int _currentLineIndex;
            private int _currentCharacter;

            public SubmissionView(ObservableCollection<string> submissionDocument)
            {
                _submissionDocument = submissionDocument;
                _submissionDocument.CollectionChanged += SubmissionDocumentChanged;
                _cursorTop = Console.CursorTop;

            }

            private void SubmissionDocumentChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                Render();
            }

            private void Render()
            {
                Console.SetCursorPosition(0, _cursorTop);
                Console.CursorVisible = false;
                
                var isFirst = true;
                var lineCount = 0;

                foreach (var line in _submissionDocument)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if (lineCount == 0)
                    { 
                        Console.Write("» ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }

                    Console.ResetColor();
                    Console.WriteLine(line);
                    lineCount++;
                }

                var numberOfBlankLines = _renderedLineCount - lineCount;
                if (numberOfBlankLines > 0)
                {
                    var blankLine = new string(' ', Console.WindowWidth);

                    while (numberOfBlankLines > 0)
                    {
                        Console.WriteLine(blankLine);
                    }
                }

                _renderedLineCount = lineCount;

                Console.CursorVisible = true;
                UpdateCursorPosition();
            }

            private void UpdateCursorPosition()
            {
                Console.CursorTop = _cursorTop + _currentLineIndex;
                Console.CursorLeft = 2 + _currentCharacter;
            }

            public int CurrentLineIndex
            {
                get => _currentLineIndex;
                set
                {
                    if (_currentLineIndex != value)
                    {
                        _currentLineIndex = value;
                        UpdateCursorPosition();
                    }                
                }
            }
            public int CurrentCharacter 
            {
                get => _currentCharacter;
                set
                {
                    if (_currentCharacter != value)
                    _currentCharacter = value;
                    UpdateCursorPosition();
                }
            }

        }

        private string EditSubmission()
        {
            var document = new ObservableCollection<string>();
            var view = new SubmissionView(document);

            while (true)
            {
                var key = Console.ReadKey(true);
                HandleKey(key, document, view);
            }


        }

        private void HandleKey(ConsoleKeyInfo key, ObservableCollection<string> document, SubmissionView view)
        {
            //if (key.Modifiers == ConsoleModifiers.Control)
            //{
            //    switch (key.Key)
            //    {
            //        case ConsoleKey.X:
            //            HandleCtrlX(document, view); // Exit CompilationUnit
            //            break;
            //            // Add other Ctrl+ shortcuts here
            //    }
            //}
            //else
            //{ }
            if (key.Modifiers == default(ConsoleModifiers))
            {
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        HandleEnterKey(document, view);
                        break;
                    case ConsoleKey.LeftArrow:
                        HandleLeftArrowKey(document, view);
                        break;
                    case ConsoleKey.RightArrow:
                        HandleRighArrowKey(document, view);
                        break;
                    case ConsoleKey.UpArrow:
                        HandleUpArrowKey(document, view);
                        break;
                    case ConsoleKey.DownArrow:
                        HandleDownArrorKey(document, view);
                        break;
                    default:
                        if (key.KeyChar >= ' ');
                        HandleTypingKey(document, view, key.KeyChar.ToString());
                        break;      

                }
            }
        }

        private void HandleCtrlX(ObservableCollection<string> document, SubmissionView view)
        {
            ResetCompilation();
        }

        private void HandleEnterKey(ObservableCollection<string> document, SubmissionView view)
        {
            document.Add(string.Empty);
            view.CurrentCharacter = 0;
            view.CurrentLineIndex = document.Count - 1;
        }

        private void HandleLeftArrowKey(ObservableCollection<string> document, SubmissionView view)
        {
            if (view.CurrentCharacter > 0) 
                view.CurrentCharacter--;
        }

        private void HandleRighArrowKey(ObservableCollection<string> document, SubmissionView view)
        {
            var line = document[view.CurrentLineIndex];
            if (view.CurrentCharacter < line.Length - 1)
                view.CurrentCharacter++;
        }

        private void HandleUpArrowKey(ObservableCollection<string> document, SubmissionView view)
        {
            if (view.CurrentLineIndex > 0)
                view.CurrentLineIndex--;
        }

        private void HandleDownArrorKey(ObservableCollection<string> document, SubmissionView view)
        {

            if (view.CurrentLineIndex > document.Count - 1) 
                view.CurrentLineIndex++;
        }

        private void HandleTypingKey(ObservableCollection<string> document, SubmissionView view, string text)
        {
            var lineIndex = view.CurrentLineIndex;
            var start = view.CurrentCharacter;
            document[lineIndex] = document[lineIndex].Insert(start, text);
            view.CurrentCharacter += text.Length;
        }

        private string EditSubmissionOld()
        {

            StringBuilder textBuilder = new();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                if (textBuilder.Length == 0)
                    Console.Write("» ");
                else
                    Console.Write(". ");

                Console.ResetColor();


                string input = Console.ReadLine();
                var isBlank = string.IsNullOrWhiteSpace(input);

                if (textBuilder.Length == 0)
                {
                    if (isBlank)
                        return null;

                    if (input.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                    {
                        EvaluateMetaCommand(input);
                        continue;
                    }
                }

                textBuilder.AppendLine(input);
                var text = textBuilder.ToString();
                if (!IsCompletedSubmission(text))
                    continue;

                return text;
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

        protected void ResetCompilation()
        {
            _previous = null;
        }

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
