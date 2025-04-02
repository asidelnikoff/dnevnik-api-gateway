using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dnevnik.ApiGateway.Services.HttpService;

public abstract class BaseApiService
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
    };

    protected virtual string JsonSerialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, s_jsonSerializerOptions);
    }

    protected virtual T JsonDeserialize<T>(string value)
    {
        return JsonSerializer.Deserialize<T>(value, s_jsonSerializerOptions)!;
    }
}
