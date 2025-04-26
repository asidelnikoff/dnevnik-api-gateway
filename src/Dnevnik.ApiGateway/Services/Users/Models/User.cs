namespace Dnevnik.ApiGateway.Services.Users.Models;

public class User
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public string? Patronymic { get; init; }
    public required string Login { get; init; }
    public UserType Type { get; init; }
    public string? ClassName { get; init; }
    public string? Subject { get; init; }
}