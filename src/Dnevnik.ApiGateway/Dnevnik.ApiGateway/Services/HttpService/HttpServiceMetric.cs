using System.Diagnostics.Metrics;

namespace Dnevnik.ApiGateway.Services.HttpService;

public class HttpServiceMetric
{
    public const string Name = "http_service";

    public readonly Counter<long> TotalRequests;
    public readonly Histogram<long> RequestDuration;
    public readonly Histogram<long> IncomingTraffic;
    public readonly Histogram<long> OutgoingTraffic;

    public HttpServiceMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(Name);

        TotalRequests = meter.CreateCounter<long>("http_service_request_count");
        RequestDuration = meter.CreateHistogram<long>("http_service_request_duration");
        IncomingTraffic = meter.CreateHistogram<long>("http_service_incoming_traffic");
        OutgoingTraffic = meter.CreateHistogram<long>("http_service_outgoing_traffic");
    }
}
