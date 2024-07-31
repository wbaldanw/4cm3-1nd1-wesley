namespace Acme.Core.Exceptions
{
    public class InvalidOperationCustomException : Exception
    {
        public InvalidOperationCustomException(string? message) : base(message)
        {
        }
    }
}
