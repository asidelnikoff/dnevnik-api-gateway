using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;
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
        return services
            .AddUsersService()
            .AddScheduleService()
            .AddTasksService();
    }

    private static IServiceCollection AddUsersService(this IServiceCollection services)
    {
        services.AddHttpClient<IUsersApiService>((provider, client) =>
        {
            var apiClientOptions = provider
                .GetRequiredService<IOptions<UsersOptions>>()
                .Value;

            client.BaseAddress = new Uri(apiClientOptions.Url);
            client.Timeout = apiClientOptions.Timeout;
        });

        return services
            .AddTransient<IUsersApiService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(nameof(IUsersApiService));
                var logger = provider.GetRequiredService<ILogger<HttpService>>();
                var metric = provider.GetRequiredService<HttpServiceMetric>();

                var httpService = new HttpService(httpClient, nameof(IUsersApiService), logger, metric);

                return new UsersApiService(httpService);
            });
    }
    
    private static IServiceCollection AddScheduleService(this IServiceCollection services)
    {
        services.AddHttpClient<IScheduleApiService>((provider, client) =>
        {
            var apiClientOptions = provider
                .GetRequiredService<IOptions<ScheduleOptions>>()
                .Value;

            client.BaseAddress = new Uri(apiClientOptions.Url);
            client.Timeout = apiClientOptions.Timeout;
        });

        return services
            .AddTransient<IScheduleApiService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(nameof(IScheduleApiService));
                var logger = provider.GetRequiredService<ILogger<HttpService>>();
                var metric = provider.GetRequiredService<HttpServiceMetric>();

                var httpService = new HttpService(httpClient, nameof(IScheduleApiService), logger, metric);

                return new ScheduleApiService(httpService);
            });
    }
    
    private static IServiceCollection AddTasksService(this IServiceCollection services)
    {
        services.AddHttpClient<ITasksApiClient>((provider, client) =>
        {
            var apiClientOptions = provider
                .GetRequiredService<IOptions<TasksOptions>>()
                .Value;

            client.BaseAddress = new Uri(apiClientOptions.Url);
            client.Timeout = apiClientOptions.Timeout;
        });

        return services
            .AddTransient<ITasksApiClient>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(nameof(ITasksApiClient));
                var logger = provider.GetRequiredService<ILogger<HttpService>>();
                var metric = provider.GetRequiredService<HttpServiceMetric>();

                var httpService = new HttpService(httpClient, nameof(ITasksApiClient), logger, metric);

                return new TasksApiService(httpService);
            });
    }
}