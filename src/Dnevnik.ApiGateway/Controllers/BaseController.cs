using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
public class BaseController : ControllerBase
{
    [HttpPost("auth/login")]
    public LoginResponse PostLogin(LoginRequest request) => throw new NotImplementedException();
}