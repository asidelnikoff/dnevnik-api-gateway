namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class LoginRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}