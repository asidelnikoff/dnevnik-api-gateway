using Dnevnik.ApiGateway.Controllers.Dto.Requests;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class HomeworkController : BaseController
{
    [HttpPost("homework/{id}")]
    public IActionResult CreateHomework(Guid id, CreateHomeworkRequest request) => throw new NotImplementedException();

    [HttpPut("homework/{id}")]
    public IActionResult UpdateHomework(Guid id, CreateHomeworkRequest request) => throw new NotImplementedException();
    
    [HttpDelete("homework/{id}")]
    public IActionResult DeleteHomework(Guid id) => throw new NotImplementedException();
}