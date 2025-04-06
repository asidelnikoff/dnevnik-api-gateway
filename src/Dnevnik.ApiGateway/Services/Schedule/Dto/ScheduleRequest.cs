namespace Dnevnik.ApiGateway.Services.Schedule.Dto;

public class ScheduleRequest
{
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}