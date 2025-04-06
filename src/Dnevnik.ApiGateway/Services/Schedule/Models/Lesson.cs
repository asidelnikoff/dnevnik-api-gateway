namespace Dnevnik.ApiGateway.Services.Schedule.Models;

public class Lesson
{
    public Guid Id { get; init; }
    public required Subject Subject { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public DateOnly Date { get; init; }
    public required string ClassName { get; init; }
    public Guid TeacherId { get; init; }
    public Guid Task { get; init; }
}