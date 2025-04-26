namespace Dnevnik.ApiGateway.Services.HttpService;

public class HttpWithBodyRequest : BaseHttpRequest
{
    public string? Body { get; init; }
}
