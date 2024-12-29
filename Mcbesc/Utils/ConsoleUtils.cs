using System;

namespace Mcbesc.Utils
{
    internal static class ConsoleUtils
    {
        internal static void WriteError(string text)
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = foreground;
        }
    }
}
