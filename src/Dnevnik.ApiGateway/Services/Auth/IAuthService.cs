using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Services.Auth.Dto;

namespace Dnevnik.ApiGateway.Services.Auth;

public interface IAuthService
{
    Task<AuthTokens> Auth(LoginRequest request);
}