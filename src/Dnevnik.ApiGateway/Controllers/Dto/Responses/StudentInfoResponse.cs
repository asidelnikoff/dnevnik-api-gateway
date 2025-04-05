namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class StudentInfoResponse
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Class { get; init; }
}