using Mcbesc.Utils;

namespace Mcbesc
{
    internal readonly struct Error
    {
        internal readonly string message;
        internal readonly string scope;

        internal Error(string message) : this("Unknown", message) { }
        internal Error(string scope, string message)
        {
            this.scope = scope;
            this.message = message;
        }

        internal void Write()
        {
            ConsoleUtils.WriteError($"ERROR [{scope}] {message}");
        }
    }
}
