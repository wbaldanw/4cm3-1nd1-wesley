namespace Acme.Core.Exceptions
{
    public class NotFoundCustomException : Exception
    {
        public NotFoundCustomException(string? message) : base(message)
        {
        }
    }
}
