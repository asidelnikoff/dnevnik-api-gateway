namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateHomeworkRequest
{
    public Guid ScheduleId { get; init; }
    public required string Homework { get; init; }
}