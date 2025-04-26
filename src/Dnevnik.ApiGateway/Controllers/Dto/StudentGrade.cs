namespace Dnevnik.ApiGateway.Controllers.Dto;

public class StudentGrade
{
    public Guid StudentId { get; init; }
    public required GradeDto[] Grades { get; init; }
}