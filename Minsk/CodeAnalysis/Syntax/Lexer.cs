using Minsk.CodeAnalysis.Syntax.Core;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Text;
using System.Collections.Immutable;
using static System.Net.Mime.MediaTypeNames;

namespace Minsk.CodeAnalysis.Syntax;

internal sealed class Lexer
{               
    private readonly DiagnosticBag _diagnostics = new();
    private readonly SourceText _text;

    private int _position;
    private int _start;
    private SyntaxKind _kind;
    private object _value;

    public Lexer(SourceText text)
    {
        _text = text;
    }

    public DiagnosticBag Diagnostics => _diagnostics;

    private char Current => Peek(0);

    private char Lookahead => Peek(1);


    private char Peek(int offset)
    {
        var index = _position + offset;

        if (index >= _text.Length)
            return '\0';
        return _text[index];
    }

    private void Next()
    {
        _position++;
    }

    public SyntaxToken Lex()
    {
        _start = _position;
        _kind = SyntaxKind.BadToken;
        _value = null;

        switch (true)
        {
            case true when TryReadEndOfFile():
            case true when TryReadWhiteSpace():
            case true when TryReadNumber():
            case true when TryReadIdentifierOrKeyword():
            case true when TryReadOperator():
                break;

            default:
                _diagnostics.ReportBadCharacter(_position, Current);
                _position++;
                break;
        }


        var length = _position - _start;
        var text = SyntaxQuery.GetText(_kind);
        if (text == null)
            text = _text.ToString(_start, length);


        var token = new SyntaxToken(_kind, _start, text, _value);
        
        return token;

    }
    #region TryReadTokens
    private bool TryReadEndOfFile()
    {
        if (Current == '\0')
        {
            _kind = SyntaxKind.EndOfFileToken;
            return true;
        }
        return false;
    }

    private bool TryReadWhiteSpace()
    {
        if (Current == ' ' || Current == '\t' || Current == '\n' || Current == '\r' || char.IsWhiteSpace(Current))
        {
            ReadWhiteSpaceToken();
            return true;
        }
        return false;
    }
    private bool TryReadNumber()
    {
        if (char.IsDigit(Current))
        {
            ReadNumberToken();
            return true;
        }
        return false;
    }

    private bool TryReadIdentifierOrKeyword()
    {
        if (char.IsLetter(Current) || Current == '_')
        {
            ReadIdentifierTokenOrKeyword();
            return true;
        }
        return false;
    }

    private bool TryReadOperator()
    {
        // Try two-character operators first (greedy matching)
        var twoChar = Current.ToString() + Lookahead.ToString();

        if (SyntaxQuery.GetTokenIndex().TryGetValue(twoChar, out var kind))
        {
            _kind = kind;
            _position += 2;
            return true;
        }

        // Try single-character operators
        var oneChar = Current.ToString();

        if (SyntaxQuery.GetTokenIndex().TryGetValue(oneChar, out kind))
        {
            _kind = kind;
            _position++;
            return true;
        }

        return false;
    }
    #endregion
    #region ReadTokens

    private void ReadIdentifierTokenOrKeyword()
    {
        while (char.IsLetterOrDigit(Current) || Current == '_')
            Next();

        var length = _position - _start;
        var text = _text.ToString(_start, length);
        _kind = SyntaxQuery.GetKeywordKind(text);
    }

    private void ReadWhiteSpaceToken()
    {
        while (char.IsWhiteSpace(Current))
            Next();
        _kind = SyntaxKind.WhiteSpaceToken;
    }

    private void ReadNumberToken()
    {
        while (char.IsDigit(Current))
            Next();

        var length = _position - _start;
        var text = _text.ToString(_start, length);
        if (!int.TryParse(text, out var value))
        {
            _diagnostics.ReportInvalidNumber(new TextSpan(_start, length), text, typeof(int));
        }

        _value = value;
        _kind = SyntaxKind.NumberToken;
    }
    #endregion
}
