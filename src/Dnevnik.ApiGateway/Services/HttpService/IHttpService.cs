namespace Dnevnik.ApiGateway.Services.HttpService;

public interface IHttpService
{
    Task<string> PostAsync(HttpPostRequest request);
    Task<string> GetAsync(HttpGetRequest request);
}
