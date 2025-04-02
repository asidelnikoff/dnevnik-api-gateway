using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class GatewayController : BaseController
{
    [HttpGet("hello")]
    public string GetHello()
    {
        return "Hello";
    }
}