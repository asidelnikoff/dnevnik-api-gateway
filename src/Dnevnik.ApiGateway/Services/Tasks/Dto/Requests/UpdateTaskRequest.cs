namespace Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;

public class UpdateTaskRequest
{
    public required string Class { get; set; }
    public required Guid ClassTaskId { get; set; }
    public string? Payload { get; set; }
}