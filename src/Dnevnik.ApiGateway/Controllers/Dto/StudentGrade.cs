namespace Dnevnik.ApiGateway.Controllers.Dto;

public class StudentGrade
{
    public Guid StudentId { get; init; }
    public int Grade { get; init; }
    public GradeType Type { get; init; }
}