using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

public class TasksHealthCheck(IApiServiceFactory apiServiceFactory) : ApiHealthCheck
{
    protected override async Task MakeRequest()
    {
        var scheduleApiService = apiServiceFactory.CreateTasksApiService("health-check", false);
        await scheduleApiService.GetTaskOrDefault(Guid.Empty);
    }
}
