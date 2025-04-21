using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Tasks.Dto;
using Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;
using Dnevnik.ApiGateway.Services.Tasks.Dto.Responses;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Services.Tasks;

public class TasksApiService(IHttpService httpService) : BaseApiService, ITasksApiService
{
    public async Task<Task> CreateTask(CreateTask task)
    {
        var taskResponse = JsonDeserialize<CreateTaskTemplateResponse>(await httpService.PostAsync(new HttpPostRequest
        {
            Route = "task",
            Body = JsonSerialize(new CreateTaskTemplateRequest
            {
                Deadline = task.Deadline,
                Payload = task.Payload
            })
        }));

        await httpService.PostAsync(new HttpPostRequest
        {
            Route = "task/assignment",
            Body = JsonSerialize(new AssignTaskRequest
            {
                AssignTo = [new AssignToRequest { Class = task.Class, LessonId = task.LessonId }],
                TemplateTaskId = taskResponse.Id
            })
        });

        return await GetTask(taskResponse.Id);
    }

    public async Task<Task> GetTask(Guid id)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"task/{id}"
        });
        
        return JsonDeserialize<Task>(response);
    }

    public async Task<Task> UpdateTask(Task updatedTask)
    {
        var response = await httpService.PutAsync(new HttpPostRequest
        {
            Route = "task/update",
            Body = JsonSerialize(updatedTask)
        });
        
        return JsonDeserialize<Task>(response);
    }

    public async System.Threading.Tasks.Task DeleteTask(Guid id)
    {
        await httpService.DeleteAsync(new DeleteHttpRequest
        {
            Route = $"task/{id}/delete"
        });
    }
}