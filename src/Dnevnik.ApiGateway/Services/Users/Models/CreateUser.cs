namespace Dnevnik.ApiGateway.Services.Users.Models;

public class CreateUser
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? MiddleName { get; init; }
    public string? Login { get; init; }
    public UserType UserType { get; init; }
    public string? Password { get; init; }
}