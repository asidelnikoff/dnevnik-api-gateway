using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Tasks;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Microsoft.AspNetCore.Mvc;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Controllers;

public class HomeworkController(IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpPost("schedule/{id:guid}/homework")]
    public IActionResult CreateHomework(Guid id, CreateHomeworkRequest request)
    {
        apiServiceFactory.CreateTasksApiService(nameof(HomeworkController))
            .CreateTask(new CreateTask
            {
                Payload = request.Homework,
                LessonId = id,
                Class = "", // todo fill from session
                Deadline = DateTime.Now // todo get lesson date and fill deadline
            });

        return Ok();
    }

    [HttpPut("schedule/{id:guid}/homework")]
    public IActionResult UpdateHomework(Guid id, CreateHomeworkRequest request)
    {
        apiServiceFactory.CreateTasksApiService(nameof(HomeworkController)).UpdateTask(new Task
        {
            Deadline = DateTime.Now, // todo get lesson date and fill deadline
            Payload = request.Homework
        });
        
        return Ok();
    }

    [HttpDelete("schedule/{id:guid}/homework")]
    public IActionResult DeleteHomework(Guid id)
    {
        apiServiceFactory.CreateTasksApiService(nameof(HomeworkController)).DeleteTask(Guid.Empty); // todo get lesson info and fill taskId
        
        return Ok();
    }
}