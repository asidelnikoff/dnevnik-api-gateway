using Dnevnik.ApiGateway.Services.Users.Models;

namespace Dnevnik.ApiGateway.Services.Users.Dto;

public class FilterRequest
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Patronymic { get; init; }
    public UserType? Type { get; init; }
    public string? Login { get; init; }
    public string? ClassName { get; init; }
    public string? Subject { get; init; }
}