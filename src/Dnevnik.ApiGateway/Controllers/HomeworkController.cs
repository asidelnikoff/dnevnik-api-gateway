using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Exceptions;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Tasks.Dto;
using Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Controllers;

[Authorize]
public class HomeworkController(IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpPost("schedule/{id:guid}/homework")]
    public async System.Threading.Tasks.Task CreateHomework(Guid id, CreateHomeworkRequest request)
    {
        var lesson = await apiServiceFactory.CreateScheduleApiService(nameof(HomeworkController))
            .GetLesson(id);
        await apiServiceFactory.CreateTasksApiService(nameof(HomeworkController))
            .CreateTask(new CreateTask
            {
                Payload = request.Homework,
                LessonId = id,
                Class = lesson.ClassName,
                Deadline = lesson.Date.ToDateTime(lesson.StartTime, DateTimeKind.Utc)
            });
    }

    [HttpPut("schedule/{id:guid}/homework")]
    public async System.Threading.Tasks.Task UpdateHomework(Guid id, CreateHomeworkRequest request)
    {
        var lesson = await apiServiceFactory.CreateScheduleApiService(nameof(HomeworkController))
            .GetLesson(id);
        await apiServiceFactory.CreateTasksApiService(nameof(HomeworkController)).UpdateTask(new UpdateTaskRequest()
        {
            ClassTaskId = lesson.TaskID!.Value,
            Class = lesson.ClassName,
            Payload = request.Homework
        });
    }

    [HttpDelete("schedule/{id:guid}/homework")]
    public async System.Threading.Tasks.Task DeleteHomework(Guid id)
    {
        var lesson = await apiServiceFactory.CreateScheduleApiService(nameof(HomeworkController))
            .GetLesson(id);
        if (lesson.TaskID is null)
        {
            throw new TaskMissingException();
        }
        
        await apiServiceFactory.CreateTasksApiService(nameof(HomeworkController)).DeleteTask(lesson.TaskID.Value);
    }
}