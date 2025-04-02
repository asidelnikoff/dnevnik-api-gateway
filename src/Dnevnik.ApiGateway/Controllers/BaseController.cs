using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
public class BaseController : ControllerBase
{
    
}