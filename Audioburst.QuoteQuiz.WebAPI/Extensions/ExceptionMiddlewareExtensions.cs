using Audioburst.QuoteQuiz.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Audioburst.QuoteQuiz.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
