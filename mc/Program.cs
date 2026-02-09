using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis.Text;
using System;
using System.Text;
using static Minsk.Repl;

namespace Minsk
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repl = new MinskRepl();
            repl.Run();

        }
    }
      
}
