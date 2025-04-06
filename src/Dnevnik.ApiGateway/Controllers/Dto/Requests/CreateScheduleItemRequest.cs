namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateScheduleItemRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public required WeekDay[] WeekDays { get; init; }
    public required string StartTime { get; init; }
    public required string EndTime { get; init; }
    public required string Class { get; init; }
    public required string Subject { get; init; }
    public Guid TeacherId { get; init; }
}