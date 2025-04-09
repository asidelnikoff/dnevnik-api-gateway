using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class OptionsExtensions
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<UsersOptions>(configuration.GetRequiredSection(UsersOptions.Users))
            .Configure<ScheduleOptions>(configuration.GetRequiredSection(ScheduleOptions.Schedule))
            .Configure<TasksOptions>(configuration.GetRequiredSection(TasksOptions.Tasks));
    }
}