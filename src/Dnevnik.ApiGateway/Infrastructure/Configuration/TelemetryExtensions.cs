using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;
using Dnevnik.ApiGateway.Infrastructure.Metrics;
using Dnevnik.ApiGateway.Infrastructure.Metrics.Health;
using Dnevnik.ApiGateway.Services.HttpService;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class TelemetryExtensions
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services, IConfiguration configuration,
        string applicationName)
    {
        services
            .AddMetrics()
            .AddTelemetryHealthCheckPublisher();

        services
            .AddOpenTelemetry()
            .ConfigureResource(configure => configure.AddService(applicationName))
            .WithMetrics(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddMeter(HttpServiceMetric.Name)
                .AddMeter(HealthMetric.Name)
                .AddMeter(AppRequestsMetric.Name)
                .AddPrometheusExporter(options =>
                {
                    var prometheusOptions = configuration.GetRequiredSection("Prometheus").Get<PrometheusOptions>()!;
                    options.ScrapeEndpointPath = prometheusOptions.ScrapeEndpointPath;
                }));

        return services;
    }

    private static IServiceCollection AddMetrics(this IServiceCollection services)
    {
        services.AddSingleton<AppRequestsMetric>();
        services.AddSingleton<HttpServiceMetric>();

        services.AddSingleton<HealthMetric>();
        services.AddSingleton<IHealthCheckPublisher, HealthCheckPublisher>();

        return services;
    }
}