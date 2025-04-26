using Dnevnik.ApiGateway.Controllers.Dto;

namespace Dnevnik.ApiGateway.Services.Schedule.Dto;

public class CreateLesson
{
    public required string Subject { get; init; }
    public required string ClassName { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public DateOnly StartPeriod { get; init; }
    public DateOnly EndPeriod { get; init; }
    public required WeekDay[] DayWeek { get; init; }
    public Guid UserId { get; init; }
    public Guid? TaskId { get; set; }
}