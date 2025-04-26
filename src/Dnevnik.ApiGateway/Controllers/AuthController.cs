using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.Auth;
using Dnevnik.ApiGateway.Services.Auth.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    [HttpPost("auth/login")]
    public async Task<LoginResponse> PostLogin(LoginRequest request)
    {
        var authResult = await authService.Auth(request);
        HttpContext.Response.Cookies.Append(ClaimType.RefreshToken.ToString(), authResult.RefreshToken);

        return new LoginResponse { Token = authResult.AccessToken, User = authResult.User };
    }
    
    [HttpPost("auth/refresh")]
    public async Task<LoginResponse> PostRefresh()
    {
        // todo get refresh token to complete refresh

        var login = HttpContext.GetLoginFromClaim() ?? throw new Exception("Email must be provided");
        var password = HttpContext.GetPasswordFromClaim() ?? throw new Exception("Password must be provided");
        HttpContext.Response.Cookies.Delete(ClaimType.RefreshToken.ToString());
        return await PostLogin(new LoginRequest { Login = login, Password = password });
    }
    
    [HttpPost("auth/logout")]
    public void PostLogout()
    {
        HttpContext.Response.Cookies.Delete(ClaimType.RefreshToken.ToString());
    }
}