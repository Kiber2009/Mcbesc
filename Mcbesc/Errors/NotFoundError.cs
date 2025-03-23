namespace Mcbesc.Errors
{
    internal class NotFoundError : Error
    {
        internal NotFoundError(string message) : base(message) { }

        internal NotFoundError(string scope, string message) : base(scope, message) { }
    }
}