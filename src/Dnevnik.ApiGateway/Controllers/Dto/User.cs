namespace Dnevnik.ApiGateway.Controllers.Dto;

public class User
{
    public Guid Id { get; init; }
    public string? Login { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? MiddleName { get; init; }
    public Role Role { get; init; }
}