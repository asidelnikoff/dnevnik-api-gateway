namespace Dnevnik.ApiGateway.Services.HttpService;

public class HttpPostRequest
{
    public required string Route { get; set; }
    public string? Body { get; set; }
    public string? Authorization { get; set; }
}
