using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class AuthController : BaseController
{
    [HttpPost("auth/login")]
    public LoginResponse PostLogin(LoginRequest request) => throw new NotImplementedException();
}