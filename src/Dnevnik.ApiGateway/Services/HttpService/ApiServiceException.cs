namespace Dnevnik.ApiGateway.Services.HttpService;

/// <summary>
/// Обобщенное исключение для <see cref="IHttpService"/>
/// </summary>
public class ApiServiceException(Exception? ex = null)
    : Exception("An error occurred while working with a remote service.", ex)
{
    public string? Answer { get; init; }
    public string? ErrorText { get; init; }
    public int? StatusCode { get; init; }
}
