namespace Dnevnik.ApiGateway.Services.Schedule.Dto;

public class MarkInfo
{
    public DateOnly Date { get; init; }
    public Guid TeacherId { get; init; }
    public Guid UserId { get; init; }
    public Guid LessonId { get; init; }
    public string? Mark { get; init; }
}