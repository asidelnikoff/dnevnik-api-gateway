namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class GradesResponse
{
    public required Student Student { get; init; }
    public int? LessonGrade { get; init; }
    public int? HomeworkGrade { get; init; }
}