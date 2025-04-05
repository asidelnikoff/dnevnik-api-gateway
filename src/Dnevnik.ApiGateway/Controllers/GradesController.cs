using Dnevnik.ApiGateway.Controllers.Dto.Requests;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class GradesController : BaseController
{
    [HttpPost("grades")]
    public IActionResult PostGrades(CreateGradeRequest request) => throw new NotImplementedException();

    [HttpPut("grades")]
    public IActionResult UpdateGrades(CreateGradeRequest request) => throw new NotImplementedException();
}