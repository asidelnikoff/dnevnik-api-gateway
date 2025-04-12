using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Dnevnik.ApiGateway.Infrastructure.Metrics;

public static class CounterExtensions
{
    public static void Inc(this Counter<long> counter)
    {
        counter.Add(1);
    }

    public static void Dec(this Counter<long> counter)
    {
        counter.Add(-1);
    }

    public static void Inc(this Counter<long> counter, string path, int statusCode)
    {
        counter.Add(1, new TagList
        {
            { "path", path },
            { "status_code", statusCode }
        });
    }

    public static void Inc(this Counter<long> counter, params KeyValuePair<string, object?>[] tags)
    {
        counter.Add(1, tags);
    }

    public static void Dec(this Counter<long> counter, params KeyValuePair<string, object?>[] tags)
    {
        counter.Add(-1, tags);
    }
}
