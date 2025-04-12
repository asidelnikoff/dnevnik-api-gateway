using System.Text.RegularExpressions;

namespace Dnevnik.ApiGateway.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static string? GetRouteTemplateOrDefault(this HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var result = (endpoint as RouteEndpoint)?.RoutePattern.RawText;

        if (result is null)
        {
            return result;
        }

        var match = Regex.Match(context.Request.Path, @"v\d+");
        var version = match.Success ? match.Value : "";

        result = Regex.Replace(result, @"v\{version:[^}]+\}", version);
        result = Regex.Replace(result, @"\{([^:}]+):[^}]+\}", ":$1");

        return result;
    }
}