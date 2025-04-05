namespace Dnevnik.ApiGateway.Controllers.Dto;

public class Teacher : User
{
    public required string Subject { get; init; }
}