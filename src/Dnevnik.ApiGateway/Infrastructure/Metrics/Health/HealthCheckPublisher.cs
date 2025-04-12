using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dnevnik.ApiGateway.Infrastructure.Metrics.Health;

public class HealthCheckPublisher(HealthMetric metric) : IHealthCheckPublisher
{
    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        foreach (var entry in report.Entries)
        {
            var tag = new KeyValuePair<string, object?>("name", entry.Key);
            metric.HealthCheckGauge.SetValue(ConvertStatus(entry.Value.Status), tag);
        }

        metric.HealthGauge.SetValue((long)report.Status);

        return Task.CompletedTask;
    }

    private long ConvertStatus(HealthStatus status) =>
        status switch
        {
            HealthStatus.Unhealthy => 0,
            HealthStatus.Degraded => 0,
            HealthStatus.Healthy => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}
