using System.Security.Claims;

using Dnevnik.ApiGateway.Services.Auth.Models;

namespace Dnevnik.ApiGateway.Extensions;

public static class HttpContextExtensions
{
    public static string? GetLoginFromClaim(this HttpContext context)
    {
        return context.GetClaimByTypeOrDefault(ClaimType.Login)?.Value;
    }
    
    public static string? GetPasswordFromClaim(this HttpContext context)
    {
        return context.GetClaimByTypeOrDefault(ClaimType.Password)?.Value;
    }
    
    public static string? GetClassFromClaim(this HttpContext context)
    {
        return context.GetClaimByTypeOrDefault(ClaimType.Class)?.Value;
    }
    
    public static Guid GetIdFromClaim(this HttpContext context)
    {
        if (!Guid.TryParse(context.GetClaimByTypeOrDefault(ClaimType.UserId)?.Value, out var result))
        {
            throw new Exception("Id must be provided");
        }
        
        return result;
    }
    
    private static Claim? GetClaimByTypeOrDefault(this HttpContext context, ClaimType type)
    {
        return context.User.Claims.FirstOrDefault(x => x.Type == type.ToString());
    }
}