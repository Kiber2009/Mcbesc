using Mcbesc.Utils;

namespace Mcbesc.Errors
{
    internal class Error
    {
        private readonly string _message;
        private readonly string _scope;

        internal Error(string message) : this("Unknown", message) { }

        internal Error(string scope, string message)
        {
            _scope = scope;
            _message = message;
        }

        internal void Write()
        {
            ConsoleUtils.WriteError($"{GetType().Name} [{_scope}] {_message}");
        }
    }
}