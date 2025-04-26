using Dnevnik.ApiGateway.Controllers.Dto;

namespace Dnevnik.ApiGateway.Services.Auth.Dto;

public class AuthTokens
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }

    public User User { get; set; }
}