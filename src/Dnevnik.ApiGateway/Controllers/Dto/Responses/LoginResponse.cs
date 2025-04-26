using Dnevnik.ApiGateway.Controllers.Dto.Requests;

namespace Dnevnik.ApiGateway.Controllers.Dto.Responses;

public class LoginResponse : RefreshReponse
{
    public required User User { get; init; }
}