namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class GradesResponse
{
    public required Student Student { get; init; }
    public GradeDto[]? Grades { get; init; }
}