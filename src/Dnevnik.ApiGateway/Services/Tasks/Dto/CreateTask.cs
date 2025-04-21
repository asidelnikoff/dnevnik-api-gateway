namespace Dnevnik.ApiGateway.Services.Tasks.Dto;

public class CreateTask
{
    public required string Class { get; init; }
    public DateTime Deadline { get; init; }
    public Guid LessonId { get; init; }
    public string? Payload { get; init; }
}