using Dnevnik.ApiGateway.Controllers.Dto;

namespace Dnevnik.ApiGateway.Services.Auth.Models;

public class AccessToken
{
    public Role UserType { get; init; }
    public required string Email { get; init; }
}