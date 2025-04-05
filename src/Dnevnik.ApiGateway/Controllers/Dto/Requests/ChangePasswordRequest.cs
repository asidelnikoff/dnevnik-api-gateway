namespace Dnevnik.ApiGateway.Controllers.Dto.Requests;

public class ChangePasswordRequest
{
    public string? NewLogin { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}