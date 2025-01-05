using System;

namespace Mcbesc.Utils
{
    internal static class ConsoleUtils
    {
        internal static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
