using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

public class UsersHealthCheck(IApiServiceFactory apiServiceFactory) : ApiHealthCheck
{
    protected override async Task MakeRequest()
    {
        var scheduleApiService = apiServiceFactory.CreateUsersApiService("health-check", false);
        await scheduleApiService.GetUserInfoAsync(Guid.Empty);
    }
}
