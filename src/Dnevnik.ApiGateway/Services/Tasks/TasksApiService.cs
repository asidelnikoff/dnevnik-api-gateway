using Dnevnik.ApiGateway.Services.HttpService;

namespace Dnevnik.ApiGateway.Services.Tasks;

public class TasksApiService(IHttpService httpService) : BaseApiService, ITasksApiClient
{
    
}