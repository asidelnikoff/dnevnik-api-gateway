namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class StuffInfoResponse
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Subject { get; init; }
    public Role Role { get; init; }
}