using Microsoft.AspNetCore.HttpLogging;

namespace Dnevnik.ApiGateway.Infrastructure;

public class HttpLoggingInterceptor : IHttpLoggingInterceptor
{
    public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
    {
        if (!IsLoggable(logContext))
        {
            logContext.LoggingFields = HttpLoggingFields.None;
        }

        return default;
    }

    public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
    {
        return default;
    }

    private bool IsLoggable(HttpLoggingInterceptorContext logContext)
    {
        return logContext.HttpContext.Request.Path.Value?.Contains("api") == true &&
               logContext.HttpContext.Request.Path.Value?.Contains("metrics") == false &&
               logContext.HttpContext.Request.Path.Value?.Contains("swagger") == false;

    }
}