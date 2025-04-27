using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Auth;
using Dnevnik.ApiGateway.Services.Journal;
using Dnevnik.ApiGateway.Services.Schedule;
using Dnevnik.ApiGateway.Services.Tasks;
using Dnevnik.ApiGateway.Services.Users;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class ServicesExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddApiServices();
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpClient<IScheduleApiService>()
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            });
        services.AddHttpClient<ITasksApiService>();
        services.AddHttpClient<IUsersApiService>();
        services.AddHttpClient<IJournalApiService>();

        return services
            .AddTransient<IApiServiceFactory, ApiServiceFactory>();
    }
}