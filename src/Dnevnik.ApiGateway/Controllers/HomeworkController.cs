using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Services.Tasks;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Microsoft.AspNetCore.Mvc;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Controllers;

public class HomeworkController(ITasksApiService tasksApiService) : BaseController
{
    [HttpPost("homework/{id}")]
    public IActionResult CreateHomework(Guid id, CreateHomeworkRequest request)
    {
        tasksApiService.CreateTask(new CreateTask
        {
            Payload = request.Homework,
            LessonId = id,
            Class = "", // todo fill from session
            Deadline = DateTime.Now // todo get lesson date and fill deadline
        });

        return Ok();
    }

    [HttpPut("homework/{id}")]
    public IActionResult UpdateHomework(Guid id, CreateHomeworkRequest request)
    {
        tasksApiService.UpdateTask(new Task()
        {
            Deadline = DateTime.Now, // todo get lesson date and fill deadline
            Payload = request.Homework
        });
        
        return Ok();
    }

    [HttpDelete("homework/{id}")]
    public IActionResult DeleteHomework(Guid id)
    {
        tasksApiService.DeleteTask(Guid.Empty); // todo get lesson info and fill taskId
        
        return Ok();
    }
}