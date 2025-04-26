namespace Dnevnik.ApiGateway.Services.Users.Dto;

public class AuthRequest
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}