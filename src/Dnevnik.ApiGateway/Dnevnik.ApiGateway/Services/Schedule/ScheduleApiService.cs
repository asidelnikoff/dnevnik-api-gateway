using Dnevnik.ApiGateway.Services.HttpService;

namespace Dnevnik.ApiGateway.Services.Schedule;

public class ScheduleApiService(IHttpService httpService) : BaseApiService, IScheduleApiService
{
    
}