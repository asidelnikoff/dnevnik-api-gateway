using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;

namespace Dnevnik.ApiGateway.Services.ApiService;

public interface IApiServiceFactory
{
    TApiServiceInterface Create<TApiServiceInterface, TApiServiceType, TOptions>(
        string serviceName,
        bool isLoggingEnabled = true)
        where TApiServiceType : class, TApiServiceInterface
        where TOptions : BaseApiServiceOptions;
}