using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Tasks;
using Dnevnik.ApiGateway.Services.Tasks.Dto;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Microsoft.AspNetCore.Mvc;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Controllers;

public class HomeworkController(IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpPost("schedule/{id:guid}/homework")]
    public async Task<IActionResult> CreateHomework(Guid id, CreateHomeworkRequest request)
    {
        await apiServiceFactory.CreateTasksApiService(nameof(HomeworkController))
            .CreateTask(new CreateTask
            {
                Payload = request.Homework,
                LessonId = id,
                Class = "10", // todo fill from session
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
    public async System.Threading.Tasks.Task DeleteHomework(Guid id)
    {
        await apiServiceFactory.CreateTasksApiService(nameof(HomeworkController)).DeleteTask(id); // todo get lesson info and fill taskId
    }
}