using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class GradesController : BaseController
{
    [HttpGet("schedulte/{id:guid}/grades")]
    public GradesResponse[] GetUserGrades(Guid scheduleId) => throw new NotImplementedException();
    
    [HttpPut("schedule/{id:guid}/grades")]
    public IActionResult UpdateGrades(Guid scheduleId, CreateGradeRequest request) => throw new NotImplementedException();
}