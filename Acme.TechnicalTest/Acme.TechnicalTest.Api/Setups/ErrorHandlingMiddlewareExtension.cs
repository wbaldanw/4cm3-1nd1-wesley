using Acme.TechnicalTest.Api.Middlewares;

namespace Acme.TechnicalTest.Api.StartUps
{
    public static class ErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseHandlingExtension(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
