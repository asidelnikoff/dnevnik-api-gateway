namespace Dnevnik.ApiGateway.Services.HttpService;

public interface IHttpService
{
    Task<string> PostAsync(HttpWithBodyRequest request);
    Task<string> GetAsync(BaseHttpRequest request);
    Task<string> DeleteAsync(HttpWithBodyRequest request);
    Task<string> PutAsync(HttpWithBodyRequest request);
}
