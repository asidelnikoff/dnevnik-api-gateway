namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateScheduleItemRequest
{
    public DateOnly Date { get; init; }
    public required string StartTime { get; init; }
    public required string EndTime { get; init; }
    public required string Class { get; init; }
    public required string Subject { get; init; }
    public Guid TeacherId { get; init; }
}