using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

public class ScheduleHealthCheck(IApiServiceFactory apiServiceFactory) : ApiHealthCheck
{
    protected override async Task MakeRequest()
    {
        var scheduleApiService = apiServiceFactory.CreateScheduleApiService("health-check", false);
        await scheduleApiService.GetUserSchedule(Guid.Empty, Role.Student);
    }
}
