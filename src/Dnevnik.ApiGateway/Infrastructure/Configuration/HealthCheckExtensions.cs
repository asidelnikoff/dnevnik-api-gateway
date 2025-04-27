using System.Text.Json;
using System.Text.Json.Serialization;

using Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddApplicationLifecycleHealthCheck()
            .AddCheck<ScheduleHealthCheck>("Schedule")
            .AddCheck<TasksHealthCheck>("Tasks")
            .AddCheck<UsersHealthCheck>("Users")
            .AddCheck<JournalHealthCheck>("Journal");

        return services;
    }

    public static WebApplication MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var result = new
                {
                    report.Status,
                    TotalDuration = report.TotalDuration.TotalMilliseconds,
                    ServicesChecks = report.Entries.Select(entry => new
                    {
                        Name = entry.Key,
                        entry.Value.Status,
                        Duration = entry.Value.Duration.TotalMilliseconds,
                        entry.Value.Description,
                        entry.Value.Exception,
                        entry.Value.Tags
                    })
                };

                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
            }
        });
        return app;
    }
}