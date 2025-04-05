namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateUserRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string MiddleName { get; init; }
}