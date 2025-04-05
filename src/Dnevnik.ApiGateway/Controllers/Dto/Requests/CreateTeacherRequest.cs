namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class CreateTeacherRequest : CreateUserRequest
{
    public required string Subject { get; init; }
    public Role Role { get; init; } = Role.Teacher;
}