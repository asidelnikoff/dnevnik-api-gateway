using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

namespace Dnevnik.ApiGateway.Services.Schedule.Dto;

public class CreateLesson
{
    public required Subject Subject { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public required string ClassName { get; init; }
    public required WeekDay[] DayWeek { get; init; }
    public Guid TeacherId { get; init; }
}