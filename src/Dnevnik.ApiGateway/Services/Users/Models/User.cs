namespace Dnevnik.ApiGateway.Services.Users.Models;

public class User
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string Login { get; init; }
    public UserType UserType { get; init; }
    public string? Class { get; init; }
    public string? Subject { get; init; }
}