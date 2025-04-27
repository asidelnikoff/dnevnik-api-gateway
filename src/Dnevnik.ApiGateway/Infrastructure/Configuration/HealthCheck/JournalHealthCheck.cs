using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Journal.Models;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration.HealthCheck;

public class JournalHealthCheck(IApiServiceFactory apiServiceFactory) : ApiHealthCheck
{
    protected override async Task MakeRequest()
    {
        var journalApiService = apiServiceFactory.CreateJournalApiService("health-check", false);
        await journalApiService.GetMarks(new FiltersRequest { UserId = Guid.Empty });
    }
}
