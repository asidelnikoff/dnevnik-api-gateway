using Dnevnik.ApiGateway.Services.HttpService;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

public abstract class ApiHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return HealthCheckResult.Healthy();
        }

        try
        {
            await MakeRequest();
            return HealthCheckResult.Healthy();
        }
        catch (ApiServiceException ex)
        {
            if (ex.StatusCode is not null && ex.StatusCode < 500)
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
        catch (Exception)
        {
            return HealthCheckResult.Unhealthy();
        }
    }

    protected abstract Task MakeRequest();
}
