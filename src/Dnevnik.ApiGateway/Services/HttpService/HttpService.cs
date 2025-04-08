using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Dnevnik.ApiGateway.Services.HttpService;

/// <summary>
/// Сервис по работе с HTTP
/// </summary>
public class HttpService(
    HttpClient httpClient,
    string clientName,
    ILogger<HttpService> logger,
    HttpServiceMetric serviceMetric,
    Action<HttpResponseMessage>? errorHandler = null,
    HttpServiceOptions? serviceOptions = null
) : IHttpService
{
    public async Task<string> PostAsync(HttpPostRequest request)
    {
        Log(
            LogLevel.Information,
            "{0}. {1}: bodyRequest = {2}, route = {3}{4}",
            clientName,
            nameof(PostAsync),
            request.Body,
            httpClient.BaseAddress,
            request.Route
        );

        HttpContent? content = null;
        if (!string.IsNullOrEmpty(request.Body))
        {
            content = new StringContent(request.Body, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        if (!string.IsNullOrEmpty(request.Authorization))
        {
            content ??= new StringContent("");

            httpClient.DefaultRequestHeaders.Add("Authorization", request.Authorization);
        }

        return await SendRequestAsync(httpClient.PostAsync(request.Route, content), $"{httpClient.BaseAddress}{request.Route}");
    }

    public async Task<string> GetAsync(BaseHttpRequest request)
    {
        Log(
            LogLevel.Information,
            "{0}.{1}: route = {2}",
            clientName,
            nameof(GetAsync),
            request.Route
        );

        return await SendRequestAsync(httpClient.GetAsync(request.Route), $"{httpClient.BaseAddress}{request.Route}");
    }
    
    public async Task<string> DeleteAsync(BaseHttpRequest request)
    {
        Log(
            LogLevel.Information,
            "{0}.{1}: route = {2}",
            clientName,
            nameof(DeleteAsync),
            request.Route
        );

        return await SendRequestAsync(httpClient.DeleteAsync(request.Route), $"{httpClient.BaseAddress}{request.Route}");
    }

    private async Task<string> SendRequestAsync(Task<HttpResponseMessage> request, string url)
    {
        var sw = Stopwatch.StartNew();
        HttpResponseMessage? response = null;

        try
        {
            response = await request;

            var statusCode = (int)response.StatusCode;

            var answer = await response.Content.ReadAsStringAsync();

            Log(
                LogLevel.Information,
                "{0}. statusCode = {1}, answer = {2}, duration = {3}",
                clientName,
                statusCode,
                answer,
                sw.Elapsed
            );

            ValidationStatusCode(response, answer);

            return answer;
        }
        catch (Exception ex)
        {
            Log(
                LogLevel.Warning,
                "{0}.{1}. timeout = {2}, duration = {3}",
                clientName,
                nameof(SendRequestAsync),
                httpClient.Timeout.TotalSeconds,
                sw.Elapsed
            );

            if (ex is ApiServiceException)
            {
                throw;
            }

            int? statusCode = 500;
            if (ex is HttpRequestException httpRequestException)
            {
                statusCode = httpRequestException.StatusCode is not null
                    ? (int)httpRequestException.StatusCode
                    : null;
            }

            var error = new
            {
                Url = url,
                Error = ex.Message
            };

            throw new ApiServiceException(ex)
            {
                Answer = JsonSerializer.Serialize(error),
                StatusCode = statusCode
            };
        }
        finally
        {
            if (response is not null)
            {
                var tags = CreateTags(
                    httpClient.BaseAddress?.Host,
                    response.RequestMessage?.RequestUri?.LocalPath,
                    response.RequestMessage?.Method.Method,
                    (int)response.StatusCode
                );

                var responseLength = (await response.Content.ReadAsByteArrayAsync()).LongLength;
                var requestLength = response.RequestMessage?.Content is null
                    ? 0
                    : (await response.RequestMessage.Content.ReadAsByteArrayAsync()).LongLength;

                serviceMetric.TotalRequests.Add(1, tags);
                serviceMetric.RequestDuration.Record(sw.ElapsedMilliseconds, tags);
                serviceMetric.IncomingTraffic.Record(responseLength, tags);
                serviceMetric.OutgoingTraffic.Record(requestLength, tags);
            }
        }
    }

    private void ValidationStatusCode(HttpResponseMessage response, string answer)
    {
        var statusCode = (int)response.StatusCode;
        if (statusCode < 400)
        {
            return;
        }

        errorHandler?.Invoke(response);

        throw new ApiServiceException
        {
            Answer = answer,
            ErrorText = $"Получена ошибка \"{response.StatusCode}\" при запросе на {response.RequestMessage?.RequestUri}",
            StatusCode = statusCode
        };
    }

    private void Log(LogLevel level, string message, params object?[] props)
    {
        if (serviceOptions?.IsLoggingEnabled == false)
        {
            return;
        }

        logger.Log(level, message, props);
    }

    private KeyValuePair<string, object?>[] CreateTags(
        string? host,
        string? path,
        string? method,
        int statusCode)
    {
        return
        [
            new KeyValuePair<string, object?>("host", host),
            new KeyValuePair<string, object?>("method", method),
            new KeyValuePair<string, object?>("path", path),
            new KeyValuePair<string, object?>("status_code", statusCode)
        ];
    }
}
