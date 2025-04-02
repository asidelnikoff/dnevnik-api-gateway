namespace Dnevnik.ApiGateway.Infrastructure.Configuration.Config;

public class BaseApiClientOptions
{
    public required string Url { get; init; }
    public TimeSpan Timeout { get; init; }
}