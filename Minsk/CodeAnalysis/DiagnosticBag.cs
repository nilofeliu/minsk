using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Text;
using System.Collections;

namespace Minsk.CodeAnalysis
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new();
        
        private void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }

        IEnumerator<Diagnostic> IEnumerable<Diagnostic>.GetEnumerator()
        {
            return ((IEnumerable<Diagnostic>)_diagnostics).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_diagnostics).GetEnumerator();
        }
        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }
        public void ReportInvalidNumber(TextSpan textSpan, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type}.";
            Report(textSpan, message);
        }

        public void ReportBadCharacter(int position, char character)
        {
            var message = $"Bad character input: '{character}'.";
            TextSpan span = new TextSpan(position, 1);
            Report(span, message);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actalKind, SyntaxKind expectedKind)
        {
            var message = $"Unexpected token <{actalKind}>, expected <{expectedKind}>.";
            Report(span, message);

        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            var message = $"Unary operator '{operatorText}' is not defined for type '{operandType}'.";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
        {
            var message = $"Binary operator '{operatorText}' is not defined for types '{leftType}' and '{rightType}'.";
            Report(span, message);
        }

        public void ReportUndefinedName(TextSpan span, string name)
        {
            var message = $"Variable '{name}' doesn't exist.";
            Report(span, message); ;
        }


        public void ReportCannotConvert(TextSpan span, Type fromType, Type toType)
        {
            var message = $"Cannot convert type '{fromType}' to '{toType}'.";
            Report(span, message); ;
        }

        internal void ReportVariableAlreadyDeclared(TextSpan span, string name)
        {
            var message = $"Variable '{name}' is already declared.";
            Report(span, message);
        }

        internal void ReportCannotAssign(TextSpan span, string name)
        {
            var message = $"Variable '{name}' is read-only and cannot be assigned to.";
            Report(span, message);
        }

        internal void ReportDuplicateCaseLabel(TextSpan span, object value)
        {
            var message = $"The case label '{value}' already appears in this switch statement.";
            Report(span, message);
        }

        internal void ReportKeywordAsIdentifier(TextSpan span, string keyword)
        {
            var message = $"'{keyword}' is a keyword and cannot be used as an identifier.";
            Report(span, message);
        }
                
    }
}
