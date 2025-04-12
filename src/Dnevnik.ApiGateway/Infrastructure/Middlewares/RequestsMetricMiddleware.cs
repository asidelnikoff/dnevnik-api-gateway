using System.Diagnostics;

using Dnevnik.ApiGateway.Infrastructure.Extensions;
using Dnevnik.ApiGateway.Infrastructure.Metrics;

namespace Dnevnik.ApiGateway.Infrastructure.Middlewares;

class RequestsMetricMiddleware(RequestDelegate next, AppRequestsMetric requestsMetric)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Path.ToString().Contains("/metrics"))
        {
            await next(httpContext);
            return;
        }

        var sw = new Stopwatch();
        sw.Start();
        await next(httpContext);
        sw.Stop();

        var routeTemplate = httpContext.GetRouteTemplateOrDefault();
        var tags = CreateTags(routeTemplate, httpContext.Request.Method, httpContext.Response.StatusCode);

        var requestLength = httpContext.Request.ContentLength ?? 0;

        requestsMetric.TotalRequests.Inc(tags);
        requestsMetric.RequestDuration.Record(sw.ElapsedMilliseconds, tags);
    }

    private KeyValuePair<string, object?>[] CreateTags(string? path, string? method, int statusCode)
    {
        return
        [
            new KeyValuePair<string, object?>("method", method),
            new KeyValuePair<string, object?>("path", path),
            new KeyValuePair<string, object?>("status_code", statusCode)
        ];
    }
}
