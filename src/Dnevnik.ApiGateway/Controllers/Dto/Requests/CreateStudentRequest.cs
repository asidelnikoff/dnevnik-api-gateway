namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateStudentRequest : CreateUserRequest
{
    public required string Class { get; init; }
}