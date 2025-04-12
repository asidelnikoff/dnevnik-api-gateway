using System.Diagnostics.Metrics;

namespace Dnevnik.ApiGateway.Infrastructure.Metrics;

public class AppRequestsMetric
{
    public const string Name = "app_request";

    public readonly Counter<long> TotalRequests;
    public readonly Histogram<long> RequestDuration;
    
    public AppRequestsMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(Name);

        TotalRequests = meter.CreateCounter<long>("app_request_count", "count");
        RequestDuration = meter.CreateHistogram<long>("app_request_duration", "milliseconds");
    }
}
