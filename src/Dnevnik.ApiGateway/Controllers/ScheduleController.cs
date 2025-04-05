using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class ScheduleController : BaseController
{
    [HttpGet("schedule")]
    public ScheduleItem GetSchedule(DateTime date) => throw new NotImplementedException();

    [HttpPost("schedule")]
    public ScheduleItem CreateNewScheduleItem(CreateScheduleItemRequest request) => throw new NotImplementedException();

    [HttpPut("schedule/{id}")]
    public ScheduleItem UpdateScheduleItem(Guid id, CreateScheduleItemRequest request) => throw new NotImplementedException();

    [HttpDelete("schedule/{id}")]
    public IActionResult DeleteScheduleItem(Guid id) => throw new NotImplementedException();
}