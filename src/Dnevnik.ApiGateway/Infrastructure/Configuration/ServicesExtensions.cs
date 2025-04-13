using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Schedule;
using Dnevnik.ApiGateway.Services.Tasks;
using Dnevnik.ApiGateway.Services.Users;

using Microsoft.Extensions.Options;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class ServicesExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddApiServices();
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpClient<IScheduleApiService>();
        services.AddHttpClient<ITasksApiService>();
        services.AddHttpClient<IUsersApiService>();

        return services.AddTransient<IApiServiceFactory, ApiServiceFactory>();
    }
}