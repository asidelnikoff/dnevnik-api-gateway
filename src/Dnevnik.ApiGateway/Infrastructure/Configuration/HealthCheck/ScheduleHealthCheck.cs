using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Schedule.Dto;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

public class ScheduleHealthCheck(IApiServiceFactory apiServiceFactory) : ApiHealthCheck
{
    protected override async Task MakeRequest()
    {
        var scheduleApiService = apiServiceFactory.CreateScheduleApiService("health-check", false);
        await scheduleApiService.GetUserSchedule("", new ScheduleRequest());
    }
}
