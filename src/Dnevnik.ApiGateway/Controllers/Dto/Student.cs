namespace Dnevnik.ApiGateway.Controllers.Dto;

public class Student : User
{
    public required string Class { get; init; }
}