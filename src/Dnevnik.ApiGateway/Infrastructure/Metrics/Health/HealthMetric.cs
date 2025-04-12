using System.Diagnostics.Metrics;

namespace Dnevnik.ApiGateway.Infrastructure.Metrics.Health;

public class HealthMetric
{
    public const string Name = "app_health";
    public readonly Gauge<long> HealthCheckGauge;
    public readonly Gauge<long> HealthGauge;

    public HealthMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(Name);

        HealthCheckGauge = new Gauge<long>(meter, "app_health_check");
        HealthGauge = new Gauge<long>(meter, "app_health_overall");
    }
}
