using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Schedule;
using Dnevnik.ApiGateway.Services.Tasks;
using Dnevnik.ApiGateway.Services.Users;

namespace Dnevnik.ApiGateway.Extensions;

public static class ApiServiceExtensions
{
    public static IScheduleApiService CreateScheduleApiService(this IApiServiceFactory apiServiceFactory, string serviceName, bool isLoggingEnabled = true) =>
        apiServiceFactory.Create<IScheduleApiService, ScheduleApiService, ScheduleOptions>(serviceName, isLoggingEnabled);
    public static ITasksApiService CreateTasksApiService(this IApiServiceFactory apiServiceFactory, string serviceName, bool isLoggingEnabled = true) =>
        apiServiceFactory.Create<ITasksApiService, TasksApiService, TasksOptions>(serviceName, isLoggingEnabled);
    public static IUsersApiService CreateUsersApiService(this IApiServiceFactory apiServiceFactory, string serviceName, bool isLoggingEnabled = true) =>
        apiServiceFactory.Create<IUsersApiService, UsersApiService, UsersOptions>(serviceName, isLoggingEnabled);
}