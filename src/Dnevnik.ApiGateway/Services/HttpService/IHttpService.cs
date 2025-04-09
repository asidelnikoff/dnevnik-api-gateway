namespace Dnevnik.ApiGateway.Services.HttpService;

public interface IHttpService
{
    Task<string> PostAsync(HttpPostRequest request);
    Task<string> GetAsync(BaseHttpRequest request);
    Task<string> DeleteAsync(BaseHttpRequest request);
    Task<string> PutAsync(HttpPostRequest request);
}
