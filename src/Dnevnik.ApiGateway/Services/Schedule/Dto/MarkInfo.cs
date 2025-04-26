namespace Dnevnik.ApiGateway.Services.Schedule.Dto;

public class MarkInfo
{
    public DateOnly Date { get; init; }
    public Guid TeacherId { get; init; }
    public Guid StudentId { get; init; }
    public Guid LessonId { get; init; }
    public int? Mark { get; init; }
    public string? Comment { get; init; }
}