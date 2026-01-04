namespace Minsk.CodeAnalysis
{
    class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken NextToken()
        {
            // <numbers>
            // + - * / ( )
            // <whitespace>

            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);


            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current))
                    Next();
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _diagnostics.Add($"ERROR: The number {text} isn't valid Int32. <LEXER>");
                }

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }
            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                    Next();
                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            if (Current == '+')
            {
                var token = new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);

                return token;
            }
            else if (Current == '-')
            {
                var token = new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);

                return token;
            }
            else if (Current == '*')
            {
                var token = new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);

                return token;
            }
            else if (Current == '/')
            {
                var token = new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);

                return token;
            }
            else if (Current == '(')
            {
                var token = new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);

                return token;
            }
            else if (Current == ')')
            {
                var token = new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);

                return token;
            }

            _diagnostics.Add($"ERROR: bad character input: '{Current}' at position {_position}. <LEXER>");

            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);

        }
    }
}
