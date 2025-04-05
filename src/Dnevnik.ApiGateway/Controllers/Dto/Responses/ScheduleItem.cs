namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class ScheduleItem
{
    public Guid Id { get; init; }
    public DateOnly Date { get; init; }
    public required string StartTime { get; init; }
    public required string EndTime { get; init; }
    public required string Subject { get; init; }
    public required Teacher Teacher { get; init; }
    public required string Class { get; init; }
    public string? Homework { get; init; }
    public int? LessonGrade { get; init; }
    public int? HomeworkGrade { get; init; }
}