using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;
using Dnevnik.ApiGateway.Services.HttpService;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class TelemetryExtensions
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services, IConfiguration configuration, string applicationName)
    {
        services
            .AddMetrics();

        services
            .AddOpenTelemetry()
            .ConfigureResource(configure => configure.AddService(applicationName))
            .WithMetrics(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddMeter(HttpServiceMetric.Name)
                .AddPrometheusExporter(options =>
                {
                    var prometheusOptions = configuration.GetRequiredSection("Prometheus").Get<PrometheusOptions>()!;
                    options.ScrapeEndpointPath = prometheusOptions.ScrapeEndpointPath;
                }));

        return services;
    }

    private static IServiceCollection AddMetrics(this IServiceCollection services)
    {
        services.AddSingleton<HttpServiceMetric>();
        
        return services;
    }
}