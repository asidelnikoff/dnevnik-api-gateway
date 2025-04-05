namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateGrade
{
    public Guid ScheduleId { get; init; }
    public required StudentGrade[] Grades { get; init; }
}