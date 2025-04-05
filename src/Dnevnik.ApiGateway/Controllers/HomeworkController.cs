using Dnevnik.ApiGateway.Controllers.Dto.Requests;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class HomeworkController : BaseController
{
    [HttpPost("homework")]
    public IActionResult CreateHomework(CreateHomeworkRequest request) => throw new NotImplementedException();

    [HttpPut("homework")]
    public IActionResult UpdateHomework(CreateHomeworkRequest request) => throw new NotImplementedException();

    [HttpDelete("homework/{id}")]
    public IActionResult DeleteHomework(Guid id) => throw new NotImplementedException();
}