using Dnevnik.ApiGateway.Infrastructure.Configuration.Config;
using Dnevnik.ApiGateway.Services.ApiService.Exceptions;
using Dnevnik.ApiGateway.Services.HttpService;

using Microsoft.Extensions.Options;

namespace Dnevnik.ApiGateway.Services.ApiService;

public class ApiServiceFactory(
    IServiceProvider configurationProvider,
    ILogger<HttpService.HttpService> logger,
    HttpServiceMetric metric,
    IHttpClientFactory httpClientFactory
) : IApiServiceFactory
{
    public TApiServiceInterface Create<TApiServiceInterface, TApiServiceType, TOptions>(
        string serviceName,
        bool isLoggingEnabled = true)
        where TApiServiceType : class, TApiServiceInterface
        where TOptions : BaseApiServiceOptions
    {
        var configuration = configurationProvider.GetRequiredService<IOptions<TOptions>>();
        var httpClient = httpClientFactory.CreateClient(typeof(TApiServiceInterface).Name);
        httpClient.BaseAddress = new Uri(configuration.Value.BaseUrl);
        httpClient.Timeout = configuration.Value.Timeout;

        var httpService = new HttpService.HttpService(
            httpClient,
            serviceName,
            logger,
            metric,
            null,
            new HttpServiceOptions { IsLoggingEnabled = isLoggingEnabled }
        );

        return (TApiServiceType?)Activator.CreateInstance(typeof(TApiServiceType), [httpService]) ??
               throw new UnableToInstantiateServiceException(nameof(TApiServiceInterface));
    }
}