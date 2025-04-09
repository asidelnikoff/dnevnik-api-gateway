namespace Dnevnik.ApiGateway.Services.HttpService;

public class HttpPostRequest : BaseHttpRequest
{
    public string? Body { get; init; }
    public string? Authorization { get; set; }
}
