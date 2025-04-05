namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class ChangePasswordRequest
{
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}