namespace Dnevnik.ApiGateway.Services.ApiService.Exceptions;

public class UnableToInstantiateServiceException(string serviceName)
    : Exception($"Unable to create instance of service {serviceName}");