namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class LoginResponse
{
    public required string Token { get; init; }
    public required User User { get; init; }
}